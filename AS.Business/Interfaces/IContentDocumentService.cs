using AS.Entities.Dtos;
using AS.Entities.Enums;


namespace AS.Business.Interfaces
{


    public interface IContentDocumentService
    {
        Task ProcessDocumentListAsync(List<DocumentSummeryDto>? documentList, Guid contentId, DocumentType documentType);
        Task ProcessDocumentIdtListAsync(List<Guid>? documentIdtList, Guid contentId);
        Task UpdateDocumentIdtListAsync(List<Guid>? documentIdtList, Guid contentId);
        Task UpdateDocumentListAsync(List<DocumentSummeryDto>? documentList, Guid contentId, DocumentType documentType);
    }
}
