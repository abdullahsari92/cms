using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.PublicUI.Dtos.Announcement;
using AS.Entities.PublicUI.Dtos.News;
using AutoMapper;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using ServiceStack;

namespace AS.Business
{
    public class SliderManager : BaseManager<Slider, SliderDto>, ISliderService
    {
        private readonly IDocumentService _documentService;

        public SliderManager(IRepository<Slider> repository, IMapper mapper, IRedisService redisService, IDocumentService documentService = null) : base(repository, mapper, redisService)
        {
            _documentService = documentService;
        }

        public async Task<SliderDto> GetById(Guid id)
        {
            var query = await _repository.GetAll(a => a.Id == id);
            var slider = await query .Include(d => d.Document)
                                    .FirstOrDefaultAsync();

            var SliderDto = _mapper.Map<SliderDto>(slider);
            SliderDto.DocumentSummeryDto = new DocumentSummeryDto
            {
                Path = slider.Document.Path.ToFullUrl(),
                Description = slider.Document.Description,
                Name = slider.Document.Name,
                Id = slider.Document.Id
            };

                                               

            return SliderDto;
        }

        public async Task<IDataResult<SliderDto>> AddSliders(SliderDto sliderDto)
        {
            var unitId = sliderDto.UnitId == Guid.Empty ? UserInfoExtensions.GetUnitId() : sliderDto.UnitId;

            sliderDto.UnitId = unitId ?? UserInfoExtensions.GetUnitId();

            sliderDto.Id = Guid.NewGuid();
            if (sliderDto.DocumentSummeryDto != null)
            {
                    var documentResult = await _documentService.Insert(new DocumentDto
                    {
                        Base64 = sliderDto.DocumentSummeryDto.Base64,
                        Name = sliderDto.DocumentSummeryDto.Name,
                        Description = sliderDto.DocumentSummeryDto.Description,
                        DocumentType = DocumentType.Slider.AsString(),
                        UnitId = unitId
                    });

                    sliderDto.DocumentId = documentResult.Data.Id;
            }
            var model = await this.BaseInsert(sliderDto) ?? new SliderDto();

            return new SuccessDataResult<SliderDto>(model);
        }

        public async Task<IDataResult<SliderDto>> UpdateSlider(SliderDto sliderDto)
        {
            var unitId = sliderDto.UnitId == Guid.Empty ? UserInfoExtensions.GetUnitId() : sliderDto.UnitId;

            sliderDto.UnitId = unitId ?? UserInfoExtensions.GetUnitId();
            var slider = await _repository.GetAsync(s => s.Id == sliderDto.Id);
            if (slider == null)
            {
                return new ErrorDataResult<SliderDto>("Slider bulunamadı");
            }


            var activeDocuments = sliderDto.DocumentSummeryDto;

            if (activeDocuments != null )
            {
             
                var documentSummery = activeDocuments;

         
                var documentResult = await _documentService.Insert(new DocumentDto
                {
                    Base64 = documentSummery.Base64,
                    Name = documentSummery.Name,
                    Description = documentSummery.Description,
                    DocumentType = DocumentType.Slider.AsString(),
                    UnitId = unitId
                });

             
                sliderDto.DocumentId = documentResult.Data.Id;
            }

               _mapper.Map(sliderDto, slider);

        
            await _repository.UpdateAsync(slider);

            return new SuccessDataResult<SliderDto>(sliderDto, "Slider başarıyla güncellendi.");
        }
           



    }
}
