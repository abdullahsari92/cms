using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Business.Interfaces
{
    public interface IPagesService: IBaseService<Pages,PagesDto>
    {
        Task<IDataResult<PagesDto>> AddPages(PagesDto pagesDto);
    }
}
