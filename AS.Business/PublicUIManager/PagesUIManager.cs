using AS.Business.Interfaces;
using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Pages;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business.PublicUIManager
{
    public class PagesUIManager
        : BaseUIManager<Pages, PagesDtoUI>, IPagesUIService
    {
        public PagesUIManager(
            IRepository<Pages> repository,
            IMapper mapper,
            IRedisService redisService
        ) : base(repository, mapper, redisService)
        {
        }

        /// <summary>
        /// Public Page Detay
        /// </summary>
        public async Task<PagesDtoUI?> GetById(Guid id)
        {
            var query = await _repository.GetAll(p =>
                p.Id == id &&
                p.IsApproved == true &&
                p.IsDeleted == false
            );

            var page = await query.FirstOrDefaultAsync();
            if (page == null)
                return null;

            var dto = _mapper.Map<PagesDtoUI>(page);

            // 🔥 SEO FALLBACK (PUBLIC UI KURALI)
            dto.SeoTitle = string.IsNullOrEmpty(page.SeoTitle)
                ? page.Title
                : page.SeoTitle;

            

            return dto;
        }

        /// <summary>
        /// Url ile Page Getir (SEO Friendly)
        /// </summary>
        public async Task<PagesDtoUI?> GetByUrl(string url)
        {
            var query = await _repository.GetAll(p =>
                p.ExternalUrl == url &&
                p.IsApproved == true &&
                p.IsDeleted == false
            );

            var page = await query.FirstOrDefaultAsync();
            if (page == null)
                return null;

            var dto = _mapper.Map<PagesDtoUI>(page);

            // 🔥 SEO FALLBACK
            dto.SeoTitle = string.IsNullOrEmpty(page.SeoTitle)
                ? page.Title
                : page.SeoTitle;

          

            return dto;
        }
    }
}
