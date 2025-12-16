using AS.Business.Interfaces;
using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Simple;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AS.Business
{
    public class FacultyManager : BaseManager<Faculty, FacultyDto>, IFacultyService
    {
            

        private IKPSService _kPSService;
        public FacultyManager(IMapper mapper, IRepository<Faculty> repository, IRedisService redisService, IKPSService kPSService) : base(repository, mapper, redisService)
        {
            _kPSService = kPSService;
        }


        public async Task<List<NameValue>> GetSelectOptions()
        {
            var listModel = new List<NameValue>();
            var query = await _repository.GetAll();

            var facultyId = UserInfoExtensions.GetFacultyId();

            if (facultyId != null)
            {
                query = query.Where(p => p.Id== facultyId);

                listModel = await query.ProjectTo<NameValue>(_mapper.ConfigurationProvider).ToListAsync();

                return listModel;
            }
            else
            {
                return await BaseGetSelectOptions();
            }      
      
        }


        public async Task<FacultyDto> BaseGetByCode(string code)
        {
            var entity = await _repository.GetAsync(p=>p.Code == code);
            if (entity == null)
            {
                return null;
            }

            var tMapTo = _mapper.Map(entity, new FacultyDto());

            return tMapTo;
        }


        public async Task<bool> SetupFacultyGetOBS()
        {

           var deger = _kPSService.getFakultyl();

            try
            {

          
            if (deger.Length > 90 && !deger.Contains("Hata"))
            {
                deger = deger.Remove(0, 12).Replace("]}", "");
                    deger = deger + ']';

                    var fakultyList = JsonSerializer.Deserialize<List<OBSFacultyDto>>(deger);

                    var fakulteList = new List<FacultyDto>();
                foreach (var item in fakultyList.ToList())
                {
                        string facultyType = item.FAK_TIP.ToString();
                        int educationTime = facultyType == "1.0" ? 4 : 2;
                    var fakulte = new FacultyDto
                    {
                        Code = item?.FAK_KOD,
                        Name = item.FAKULTE_ADI,
                        EducationTime = (byte)educationTime
                    };

                        if (!_repository.IsExist(p => p.Code == fakulte.Code))
                        {
                            fakulteList.Add(fakulte);


                        }
                 }
                    BaseInsertRange(fakulteList);

                    return true;

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
