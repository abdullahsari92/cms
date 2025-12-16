using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Core.Helpers;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Slider;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business.PublicUIManager
{
    public class SliderUIManager : BaseUIManager<Slider, SliderDtoUI>, ISliderUIService
    {
        public SliderUIManager(IRepository<Slider> repository, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
        {
        }

        public async Task<SliderDtoUI> GetById(Guid id)
        {
            var query = await _repository.GetAll(a => a.Id == id);
            var slider = await query
                                .Include(a => a.Document)
                                .FirstOrDefaultAsync();


            if (slider == null)
            {
                return null;
            }
            var sliderDto = _mapper.Map<SliderDtoUI>(slider);
            sliderDto.DocumentList = slider.Document != null
                ? new List<DocumentSummeryDto>
             {
                new DocumentSummeryDto
                {
                    Path = slider.Document.Path.ToFullUrl(),
                    Description = slider.Document.Description,
                    Name = slider.Document.Name,
                    Id=sliderDto.Id

                }

              } : new List<DocumentSummeryDto>();

            return sliderDto;
        }

        public async Task<List<SliderListDtoUI>> List(Guid? UnitId, Guid? languageId)
        {

            var defaultLanguageId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var langId = languageId ?? defaultLanguageId;


            var defaultUnitId = new Guid("e0f90674-f6b6-4370-8f0e-1fb93124d115");
            var unitId = UnitId ?? defaultUnitId;

            var query = await _repository.GetAll(x => x.LanguageId == langId
                                                      && x.Units.Id != null
                                                      && x.Units.Id == unitId
                                                     );



            var sliderDtoList =  await query.Select(p => new SliderListDtoUI
            {
                Title = p.Title,
                LanguageId = langId,
                Id=p.Id,
                DocumentSummeryDto = new DocumentSummeryDto
                {
                    Path = p.Document.Path.ToFullUrl(),
                    Description = p.Document.Description,
                    Name = p.Document.Name,
                    Id = p.Document.Id
                }
             }).ToListAsync();    

            return sliderDtoList;
        }
    }
}
