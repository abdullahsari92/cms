using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AutoMapper;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using ServiceStack.Script;


namespace AS.Business
{
    public class ContentDocumentManager : IContentDocumentService
    {
        protected IMapper _mapper;
        private readonly IRepository<ContentDocument> _repositoryContentDocument;
        private IDocumentService _documentService;

        public ContentDocumentManager(IDocumentService documentService, IRepository<ContentDocument> repositoryContentDocument = null)
        {
            _documentService = documentService;
            _repositoryContentDocument = repositoryContentDocument;

        }

        //--------------------:Add:-----------------//
        public async Task ProcessDocumentIdtListAsync(List<Guid>? documentIdtList, Guid contentId)
        {
            if (documentIdtList != null && documentIdtList.Any())
            {
                foreach (var documentId in documentIdtList)
                {

                    var contentDocument = new ContentDocument
                    {
                        Id = Guid.NewGuid(),
                        ContentId = contentId,
                        DocumentId = documentId
                    };
                    contentDocument = BaseEntityHelper.SetBaseEntitiy(contentDocument);
                    await _repositoryContentDocument.InsertAsync(contentDocument);
                }
            }
        }
        public async Task ProcessDocumentListAsync(List<DocumentSummeryDto>? documentList, Guid contentId, DocumentType documentType)
        {
            if (documentList != null && documentList.Any())
            {
                var unitId = UserInfoExtensions.GetUnitId();

                // Gelen dokümanlardan sadece Base64 değeri dolu ve Id'si boş (null ya da Guid.Empty) olanları alıyoruz. 
                // Diğer türlü, Base64 değeri olmayan dokümanlar da döngüye giriyor ve hata veriyor.
                var documentsToProcess = documentList
                    .Where(d => d.Id == Guid.Empty && !string.IsNullOrEmpty(d.Base64))
                    .ToList();


                foreach (var doc in documentsToProcess)
                {

                    var documentResult = await _documentService.Insert(new DocumentDto
                    {
                        Base64 = doc.Base64,
                        Name = doc.Name,
                        Description = doc.Description,
                        DocumentType = documentType.AsString(),
                        UnitId = unitId,
                    });

                    if (documentResult?.Data?.Id != null)
                    {
                        var contentDocument = new ContentDocument
                        {
                            Id = Guid.NewGuid(),
                            ContentId = contentId,
                            DocumentId = documentResult.Data.Id,
                        };

                        contentDocument = BaseEntityHelper.SetBaseEntitiy(contentDocument);
                        await _repositoryContentDocument.InsertAsync(contentDocument);
                    }
                }

            }
        }
        //--------------------:Add:-----------------//


        //--------------------:Update:-----------------//
        public async Task UpdateDocumentIdtListAsync(List<Guid>? documentIdtList, Guid contentId)
        {
            if (documentIdtList != null && documentIdtList.Any())
            {
                foreach (var documentId in documentIdtList)
                {

                    var contentDocumentQuery = await _repositoryContentDocument.GetAll(cd => cd.ContentId == contentId && cd.DocumentId == documentId);
                    var contentDocument = await contentDocumentQuery.FirstOrDefaultAsync();

                    if (contentDocument != null)
                    {

                        contentDocument.ContentId = contentId;
                        contentDocument.DocumentId = documentId;
                        contentDocument = BaseEntityHelper.SetBaseEntitiy(contentDocument);
                        await _repositoryContentDocument.UpdateAsync(contentDocument);
                    }
                    else
                    {

                        contentDocument = new ContentDocument
                        {
                            Id = Guid.NewGuid(),
                            ContentId = contentId,
                            DocumentId = documentId
                        };

                        contentDocument = BaseEntityHelper.SetBaseEntitiy(contentDocument);
                        await _repositoryContentDocument.InsertAsync(contentDocument);
                    }
                }
            }

            await Task.CompletedTask;
        }
        public async Task UpdateDocumentListAsync(List<DocumentSummeryDto>? documentList, Guid contentId, DocumentType documentType)
        {
            var unitId = UserInfoExtensions.GetUnitId();

            if (documentList != null && documentList.Any())
            {
                // Silinen Dokümanlar için
                var deletedDocuments = documentList.Where(d => d.IsDeleted).ToList();

                foreach (var documentDto in deletedDocuments)
                {

                    await RemoveContentDocumentRelationAsync(contentId, documentDto.Id);
                }

                // Silinmeyen Dokümanlar için
                var activeDocuments = documentList.Where(d => !d.IsDeleted).ToList();

                foreach (var documentDto in activeDocuments)
                {
                    var documentListModel = await _documentService.GetAll(default);
                    var existingDocument = documentListModel.Items
                        .FirstOrDefault(d => d.Id == documentDto.Id);

                    DocumentDto documentResult = null;


                    if (existingDocument == null)
                    {
                        var documentToInsert = new DocumentDto
                        {
                            Id = Guid.NewGuid(),
                            Base64 = documentDto.Base64,
                            Name = documentDto.Name,
                            Description = documentDto.Description,
                            DocumentType = documentType.AsString(),
                            UnitId = unitId,
                        };

                        var insertResult = await _documentService.Insert(documentToInsert);
                        documentResult = insertResult.Data;
                    }
                    else
                    {
                        documentResult = existingDocument;
                    }


                    if (documentResult != null)
                    {
                        var contentDocumentListModel = await _repositoryContentDocument.GetAll(default);
                        var existingContentDocument = contentDocumentListModel
                            .FirstOrDefault(cd => cd.ContentId == contentId && cd.DocumentId == documentResult.Id);

                        if (existingContentDocument == null)
                        {
                            var contentDocument = new ContentDocument
                            {
                                Id = Guid.NewGuid(),
                                ContentId = contentId,
                                DocumentId = documentResult.Id,
                            };

                            contentDocument = BaseEntityHelper.SetBaseEntitiy(contentDocument);
                            await _repositoryContentDocument.InsertAsync(contentDocument);
                        }
                    }
                }
            }
        }
        //--------------------:Update:-----------------//


        //--------------------:Helper Methods:-----------------//
        #region RemoveContentDocumentRelation
        // Verilen ContentId ve DocumentId ile içerik ve belge arasındaki ilişkiyi kaldırır.
        // İlgili ilişki mevcutsa, veritabanından içerik-belge ilişkisini siler.(content-document)
        private async Task RemoveContentDocumentRelationAsync(Guid contentId, Guid documentId)
        {
            var contentDocumentListModel = await _repositoryContentDocument.GetAll(default);
            var existingContentDocument = contentDocumentListModel
                .FirstOrDefault(cd => cd.ContentId == contentId && cd.DocumentId == documentId);

            if (existingContentDocument != null)
            {
                await _repositoryContentDocument.DeleteAsync(existingContentDocument);
            }
        }
        #endregion
        //--------------------:Helper Methods:-----------------//
    }
}


