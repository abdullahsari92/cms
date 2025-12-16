using AS.Business.Interfaces;
using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.Simple;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AS.Business
{
    public class DepartmentManager : BaseManager<Department, DepartmentDto>, IDepartmentService
    {

        private IKPSService _kPSService;

        private IFacultyService _faultyService;

        public DepartmentManager(IMapper mapper, IRepository<Department> repository, IRedisService redisService, IKPSService kPSService, IFacultyService faultyService) : base(repository, mapper, redisService)
        {
            _kPSService = kPSService;
            _faultyService = faultyService;
        }


        public async Task<List<NameValue>> GetSelectOptions()
        {
            var listModel = new List<NameValue>();
            var query = await _repository.GetAll();

            var unitId = UserInfoExtensions.GetUnitId();
            var roleLevel = UserInfoExtensions.GetRoleLevel();


            if (unitId != null && (int)RoleLevel.BolumSorumlusu <= roleLevel)
            {
                query = query.Where(p => p.Id == unitId);

                listModel = await query.ProjectTo<NameValue>(_mapper.ConfigurationProvider).ToListAsync();

                return listModel;
            }
            else
            {
                return await BaseGetSelectOptions();
            }

        }
        public async Task<bool> SetupDeparmentGetOBS(CancellationToken token)
        {

            var obsDeparments = _kPSService.getDepartment();

            try
            {


                if (obsDeparments.Length > 90 && !obsDeparments.Contains("Hata"))
                {
                    obsDeparments = obsDeparments.Remove(0, 12).Replace("]}", "");
                    obsDeparments = obsDeparments + ']';

                    var obsDepartmentList = JsonSerializer.Deserialize<List<OBSDepartmentDto>>(obsDeparments);

                    var facultyList =  await _faultyService.BaseGetAll(token);
                    var departmentList = new List<DepartmentDto>();
                    int eklenen = 0;
                    foreach (var item in obsDepartmentList.ToList())
                    {
                        FacultyDto faculty = facultyList.Items.FirstOrDefault(p=>p.Code == item.FAK_KOD);

                        if(faculty == null) { continue; }
                        var department = new DepartmentDto
                        {
                            Code = item?.BOL_KOD.ToString().Split(".")[0],
                            Name = item.BOLUM_ADI,
                            FacultyId = faculty.Id

                        };

                        var isDepartment = await _repository.IsExistAsync(p => p.Code == department.Code);
                        if (!isDepartment)
                        {
                            departmentList.Add(department);
                        }

                        ++eklenen;
                    }
                   return  await  BaseInsertRange(departmentList) !=0;

                }
                return false;

            }
            catch (Exception ex)
            {


                return false;
            }

        }


  

    }

}
