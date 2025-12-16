using AS.Business.Interfaces;
using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AutoMapper;
using Business.Adapters.Redis;


namespace AS.Business
{
    public class ContentManager : BaseManager<Content, ContentDto>, IContentService
    {
        public ContentManager(IRepository<Content> repository, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
        {
        }
    }
}
