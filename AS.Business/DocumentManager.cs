using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceStack;

namespace AS.Business
{
    public class DocumentManager : BaseManager<Document, DocumentDto>, IDocumentService
    {

        public DocumentManager(IRepository<Document> repository, IConfiguration config, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
        {
            //_config = config;
            //FileHelper._getBaseUrl = _config.GetSection("Document:getBaseUrl").Value ?? String.Empty;
            //FileHelper._uploadBaseUrl = _config.GetSection("Document:uploadBaseUrl").Value ?? String.Empty;
        }


        public async Task<ListModel<DocumentDto>> GetAll(CancellationToken token)
        {

            var listModel = new ListModel<DocumentDto>();
            var query = await _repository.GetAll();


            listModel.Items = await query.ProjectTo<DocumentDto>(_mapper.ConfigurationProvider).ToListAsync(token);

            listModel.Items.Map(p => p.Path = FileHelper._getBaseUrl + p.Path);


            return listModel;

        }

        public async Task<IDataResult<DocumentDto>> Insert(DocumentDto documentDto, bool autoSaveIsNotActive = false)
        {

            documentDto.MimeType = FileHelper.GetMimeTypeByBase64(documentDto.Base64);

            documentDto.Extension = FileHelper.GetExtension(documentDto.MimeType);

            documentDto.Id = Guid.NewGuid();

                 await UploadFile(documentDto); // document fiziksel kayıt yapıyor.

            documentDto = await this.BaseInsert(documentDto,autoSaveIsNotActive);

            return new SuccessDataResult<DocumentDto>(documentDto, "��lem ba�ar�l�");

        }

        public async Task<IDataResult<DocumentDto>> Update(DocumentDto model)
        {

            var entity = await _repository.GetAsync(p => p.Id == model.Id);
            var documentDto = _mapper.Map(entity, new DocumentDto());


            documentDto.MimeType = FileHelper.GetMimeTypeByBase64(model.Base64);
            documentDto.Extension = FileHelper.GetExtension(documentDto.MimeType);
            documentDto.Base64 = model.Base64;
            var deger = await UploadFile(documentDto);

            documentDto = await this.BaseUpdate(documentDto);

            return new SuccessDataResult<DocumentDto>(documentDto, "��lem ba�ar�l�");

        }

        public async Task UpdatePassiveById(Guid documentId)
        {

            var tEntity = _repository.Get(p => p.Id == documentId);
            if (tEntity == null)
            {
                return;
            }


            tEntity = BaseEntityHelper.SetBaseUpdateEntitiy(tEntity);
            tEntity.IsApproved = false;

            var documentDto = _mapper.Map(await _repository.UpdateAsync(tEntity), new DocumentDto());


        }

        public async Task<string> GetFullPath(DocumentDto model)
        {
            if (model.Path == null)
            {
                model = await BaseGetById(model.Id);
            }

            return FileHelper._getBaseUrl + model.Path;


        }

        private async Task<IDataResult<DocumentDto>> UploadFile(DocumentDto model)
        {

            var unitId = model.UnitId ?? UserInfoExtensions.GetUnitId() ?? Guid.Empty;


            var byteFile = model.Base64.Split(",")[1].ByteFromBase64();

            model.DocLength = byteFile.Length;
            string baseUrl = unitId + "/" + model.DocumentType + "/" + DateTime.Now.ToString("ddMMyyyy") + "/";
            var streamFile = byteFile.ByteToStream();

            FileHelper.CreatedNewFolder(baseUrl);

            var fullUrl = baseUrl + model.Id + model.Extension;

            model.Path = fullUrl;
            FileHelper.SaveFile(streamFile, fullUrl);

            return new SuccessDataResult<DocumentDto>(model);
        }


        private static string GetMimeTypeByBase64(DocumentDto documentDto)
        {

            var base64first = documentDto.Base64.Split('.')[0].Split("/")[1];



            return base64first;
        }
    }

}