using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;


namespace AS.Business.Interfaces
{
    public interface INewsService : IBaseService<News, NewsDto>
    {
        Task<IDataResult<NewsDto>> AddNews(NewsDto newsDto);
        Task<IDataResult<NewsDto>> UpdateNews(NewsDto newsDto);
        Task Delete(Guid contentId, Guid languageId);
        Task<NewsDto> GetById(Guid id);
        Task<ListModel<NewsDto>> GetByContentId(Guid ContentId, CancellationToken token);
    }
}
