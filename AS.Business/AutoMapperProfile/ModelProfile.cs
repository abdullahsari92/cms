using AS.Core.Helpers;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace AS.Business.AutoMapperProfile
{
    public class ModelProfile : Profile
    {
        private readonly IConfiguration _config;

        public ModelProfile(IConfiguration config)
        {

            _config = config;
            FileHelper._getBaseUrl = _config.GetSection("Document:getBaseUrl").Value ?? String.Empty;
            FileHelper._uploadBaseUrl = _config.GetSection("Document:uploadBaseUrl").Value ?? String.Empty;
            //--------------------------------------------------UnitsModel--------------------------------------------------//

            CreateMap<Units, UnitModel>().ReverseMap(); //bunu yazmazsak GtById Çalışmıyor. 
            CreateMap<UnitsDto, UnitModel>().ReverseMap();//Bunu yazmazsak da List çalışmıyor.

        }
    }
}
