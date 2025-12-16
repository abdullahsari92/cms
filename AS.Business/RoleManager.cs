using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.Models;
using AS.Entities.Simple;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class RoleManager : BaseManager<Role,RoleDto>,IRoleService
    {

        private IRepository<Permission> _permissionRepository;
        private IRepository<RolePermissionLine> _rolePermissionRepository;


        public RoleManager(IRepository<Role> repositoryRole, IMapper mapper,  IRedisService redisService,IRepository<Permission> permissionRepository, IRepository<RolePermissionLine> rolePermissionRepository) : base(repositoryRole, mapper, redisService)
        {
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }


        public async Task<ListModel<RoleDto>> GetAll()
        {
            var listModel = new ListModel<RoleDto>();

            var list = await _repository.GetAll();
            listModel.Items = await list.ProjectTo<RoleDto>(_mapper.ConfigurationProvider).OrderBy(p=>p.Level).ToListAsync();

            return listModel;
        }

        public async Task<List<NameValue>> GetSelectOptions()
        {
            var userRoleLevel = UserInfoExtensions.GetRoleLevel();


            var query = await _repository.GetAll(p => p.IsApproved && p.Level >= userRoleLevel);
            var listModel = new List<NameValue>();
            listModel = await query.OrderBy(p=>p.Level).ProjectTo<NameValue>(_mapper.ConfigurationProvider).ToListAsync();


            return listModel;
        }


        public async Task<RoleDetailModel> Get(Guid roleId, CancellationToken token)
        {
            //GetPermissionClaims(roleId);


            var rolePermissionList = await _rolePermissionRepository.GetAll();

            var userPermission = await rolePermissionList.Include(p=>p.Role).Where(p => p.Role.Id == roleId).Where(p=>p.Permission !=null).Select(p => p.Permission).ToListAsync(token);

            RoleDetailModel model = new RoleDetailModel();

            model.RoleDto = await BaseGetById(roleId);

            var listModel = new List<IGrouping<string, Permission>>();

            var query = await _permissionRepository.GetAll();

            var permissionModelList = new List<PermissionModel>() {};



            var controllerList = await query.Select(p => p.ControllerName).Distinct().ToListAsync(token);
            foreach (var item in controllerList)
            {
                var permissionModel = new PermissionModel();
                permissionModel.ControllerName = item.ToString();

                foreach (var value in Enum.GetValues(typeof(CrudActionType)))
                {
                    var permissionCrudSelected = new PermissionCrudSelected();
                    permissionCrudSelected.CRUDActionType  = (int)value;

                    foreach (var subItem in userPermission.Where(p => p.ControllerName == item.ToString()))
                    {
                        permissionModel.Checked = true;
                       if(permissionCrudSelected.CRUDActionType == subItem.CRUDActionType)
                        {
                            permissionCrudSelected.Checked = true;
                        }
                    }
                    permissionModel.ControllerCrudList.Add(permissionCrudSelected);
                }

                permissionModelList.Add(permissionModel);

            }

            model.PermissionList = permissionModelList;


            return model;
        }

        //todo burada ayný yetkiyi bir kaç defa yazýyor bakýlacak.
        public async Task<string> GetPermissionClaims(List<Guid> roleIds)
        {

            var rolePermissionList = await _rolePermissionRepository.GetAll();

            var userPermission = await rolePermissionList.Include(p => p.Role).
                Where(p => roleIds.Contains(p.Role.Id))
                .Where(p=>p.Permission !=null) // burasý left join olduðundan isDelted olan kayýtlarý null olarak getiriyor.
                .Select(p => p.Permission).ToListAsync();

            //var deger = await _rolePermissionRepository.GetAll().Join(await _permissionRepository.GetAll(),
            //    rp=>rp.PermissionId,
            //      p => p.Id, // Primary Key
            //      (rp, p) => new { rp.Role, Permission = p }
            //    );   

            string claims = "";

            foreach (var item in userPermission)
            {
                var permissionModel = new PermissionModel();

                claims += item.ControllerName +"." + Enum.GetName(typeof(CrudActionType), item.CRUDActionType);
                claims += ","; 
            }

            return claims;
        }


        public async Task<List<PermissionModel>> RolePermissionAdd(RoleDetailModel roleAddModel)
        {
            var query = await _permissionRepository.GetAll();


            var sonuc =await  RolePermissionDelete(roleAddModel.RoleDto.Id);

            if (sonuc)
            {

                foreach (var item in roleAddModel.PermissionList.Where(p=>p.Checked))
                {
                    var CRUDActionTypeList = item.ControllerCrudList.Where(s => s.Checked).Select(m => m.CRUDActionType);

                    var controllerPermissionList = await query.Where(p => p.ControllerName == item.ControllerName && CRUDActionTypeList.Contains(p.CRUDActionType)).ToListAsync();

                    foreach (var controllerPermission in controllerPermissionList)
                    {
                        var rolePermissionLine = new RolePermissionLine()
                        {
                            PermissionId = controllerPermission.Id,
                            RoleId = roleAddModel.RoleDto.Id
                        };

                        rolePermissionLine = BaseEntityHelper.SetBaseEntitiy(rolePermissionLine);

                       await  _rolePermissionRepository.InsertAsync(rolePermissionLine);
                    }
                }

                await _rolePermissionRepository.SaveChangesAsync();
            }

            return roleAddModel.PermissionList;
        }

        public async Task<bool> RolePermissionDelete(Guid roleId)
        {

           var query = await _rolePermissionRepository.GetAll();

            var roleList = await query.Where(p => p.RoleId == roleId).ToListAsync();

            if (roleList.Count()==0)
            {
                return true;
            }
            foreach (var item in roleList)
            {

              await  _rolePermissionRepository.DeleteAsync(item,true);

            }
            return true;

        }

        public async Task<List<PermissionModel>> RolePermissionUpdate(RoleDetailModel roleAddModel)
        {
            _rolePermissionRepository.Delete(roleAddModel.RoleDto.Id);


            await  RolePermissionAdd(roleAddModel);

            return roleAddModel.PermissionList;
        }


        public async Task<ListModel<PermissionModel>> GetEski(Guid roleId)
        {

          //var rolePermissionList =  _rolePermissionRepository.GetAll().ContinueWith(p => p.).Where(p=>p.Role.Id == roleId);


            var listModel = new List<IGrouping<string, Permission>> ();

            var query = await _permissionRepository.GetAll();

            var permissionModelList = new ListModel<PermissionModel>() { Items = new List<PermissionModel> {       
            } };


            var deger = query.Select(p => p.ControllerName).Distinct().ToList();
            foreach (var item in query.Select(p=>p.ControllerName).Distinct().ToList())
            {
                var permissionModel = new PermissionModel();
                permissionModel.ControllerName = item.ToString();

                foreach (var subItem in query.Where(p=>p.ControllerName ==item.ToString()))
                {
                    var aa = _mapper.Map(subItem, new PermissionDto());
                   // permissionModel.Value.Add(aa);
                }

              //  if(permissionModelList.Any(p=>p.))
                permissionModelList.Items.Add(permissionModel);
                
            }
                      


          //var aa =  query.GroupBy(p => p.ControllerName).Select(p => new PermissionModel
          //  {
          //      Key = p.Key,
          //      Value = p as List<PermissionDto>
          //  }
          //    ).ToList();

          //  listModel = await query.ProjectTo<PermissionDto>().GroupBy(p => p.ControllerName).ToList();

            return permissionModelList;
        }
    }
}


