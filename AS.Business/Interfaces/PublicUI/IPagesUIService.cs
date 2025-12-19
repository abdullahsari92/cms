using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Business.Interfaces.PublicUI
{
    public interface  IPagesUIService:IBaseUIService<Pages,PagesDtoUI>

    {
        
        Task<PagesDtoUI?> GetById(Guid id);
    }
}
