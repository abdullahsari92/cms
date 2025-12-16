using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;

namespace AS.Business.Interfaces
{
    public interface IDocumentService: IBaseService<Document, DocumentDto>
    {

        Task<IDataResult<DocumentDto>> Insert(DocumentDto model, bool autoSaveIsNotActive = false);

        Task UpdatePassiveById(Guid documentId);


        Task<IDataResult<DocumentDto>> Update(DocumentDto documentDto);


        Task<ListModel<DocumentDto>> GetAll(CancellationToken token);

     

    }
}
