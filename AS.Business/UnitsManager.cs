using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class UnitsManager : BaseManager<Units, UnitsDto>, IUnitsService
    {
        private IDocumentService _documentService;

        public UnitsManager(IRepository<Units> repository, IMapper mapper, IRedisService redisService, IDocumentService documentService) : base(repository, mapper, redisService)
        {
            _documentService = documentService;
        }
        public async Task<UnitModel> GetUnitByUrl(string unitUrl, CancellationToken token)
        {
          var unitQuery =await   _repository.GetAll(p => p.Url == unitUrl);
            var unit = await unitQuery.FirstOrDefaultAsync(token);

            var unitModel = _mapper.Map<UnitModel>(unit);

            return unitModel;
        }


        public async Task<IDataResult<UnitsDto>> AddUnits(UnitsDto unitsDto)
        {
            unitsDto.Id = Guid.NewGuid();

            if (unitsDto.LogoDocumentSummery != null && !string.IsNullOrEmpty(unitsDto.LogoDocumentSummery.Base64))
            {
                var logoResult = await _documentService.Insert(new DocumentDto
                {
                    Base64 = unitsDto.LogoDocumentSummery.Base64,
                    Name = unitsDto.LogoDocumentSummery.Name,
                    Description = unitsDto.LogoDocumentSummery.Description,
                    DocumentType = DocumentType.Units.AsString(),
                    UnitId = unitsDto.Id,
                    Id=Guid.NewGuid(),
               
                });
                unitsDto.LogoDocumentId = logoResult.Data.Id;
            }

            if (unitsDto.LogoTwoDocumentSummery != null && !string.IsNullOrEmpty(unitsDto.LogoTwoDocumentSummery.Base64))
            {
                var logoTwoResult = await _documentService.Insert(new DocumentDto
                {
                    Base64 = unitsDto.LogoTwoDocumentSummery.Base64,
                    Name = unitsDto.LogoTwoDocumentSummery.Name,
                    Description = unitsDto.LogoTwoDocumentSummery.Description,
                    DocumentType = DocumentType.Units.AsString(),
                    Id =Guid.NewGuid(),

                });

                unitsDto.LogoTwoDocumentId = logoTwoResult.Data.Id;
            }

            var model2 = await this.BaseInsert(unitsDto) ?? new UnitsDto();

            return new SuccessDataResult<UnitsDto>(model2, "İşlem başarıyla gerçekleşti.");
        }

        public async Task<UnitModel> GetById(Guid id)
        {
            var unitQuery = await _repository.GetAll(x => x.Id == id);
            var unit = await unitQuery
                .Include(u => u.LogoDocument)
                .Include(u => u.LogoTwoDocument)
                
                .FirstOrDefaultAsync();
         
            if (unit == null)
                return null;

            var unitModel = _mapper.Map<UnitModel>(unit);
            if (unit.LogoDocument != null)
            {
                unitModel.LogoDocumentSummery = new DocumentSummeryDto
                {
                    Name = unit.LogoDocument.Name,
                    Description = unit.LogoDocument.Description,
                    Path = unit.LogoDocument.Path.ToFullUrl(),
                    Id=unit.LogoDocument.Id ,
         
                 
                };
            }

            if (unit.LogoTwoDocument != null)
            {
                unitModel.LogoTwoDocumentSummery = new DocumentSummeryDto
                {
                    Name = unit.LogoTwoDocument.Name,
                    Description = unit.LogoTwoDocument.Description,
                    Path = unit.LogoTwoDocument.Path.ToFullUrl(),
                    Id=unit.LogoTwoDocumentId.Value,
        
             
                };
            }
            return unitModel;
        }

        public async Task<IDataResult<UnitsDto>> UpdateUnits(UnitsDto unitsDto)
        {
            return null!;
        }
    }
}
