using AS.Core.Helpers;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Activity;
using AS.Entities.PublicUI.Dtos.Announcement;
using AS.Entities.PublicUI.Dtos.Menu;
using AS.Entities.PublicUI.Dtos.News;
using AS.Entities.PublicUI.Dtos.Slider;
using AS.Entities.Simple;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace AS.Business.AutoMapperProfile
{
    public class BusinessProfile : Profile
    {
        private readonly IConfiguration _config;

        public BusinessProfile(IConfiguration config)
        {

            _config = config;
            FileHelper._getBaseUrl = _config.GetSection("Document:getBaseUrl").Value ?? String.Empty;
            FileHelper._uploadBaseUrl = _config.GetSection("Document:uploadBaseUrl").Value ?? String.Empty;


            CreateMap<Person, PersonDto>()
              .ForMember(x => x.Username, opt => opt.MapFrom(p => $"{p.User.Username}"))
                .ForMember(x => x.Email, opt => opt.MapFrom(p => $"{p.User.Email}"))
                //.ForMember(x => x.DepartmentName, opt => opt.MapFrom(p => $"{p.Department.Name}"))
                //.ForMember(x => x.FacultyName, opt => opt.MapFrom(p => $"{p.Department.Faculty.Name}"))
                .ForMember(x => x.UserType, opt => opt.MapFrom(p => p.User.UserType))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(p => String.Join(",  ", p.User.RoleUserLines.Where(p => p.UnitId == null).OrderBy(m => m.Role.Level).Select(p => p.Role.Name))))
                .ReverseMap();



            CreateMap<PersonDto, Person>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore())
            ;

            CreateMap<Faculty, FacultyDto>().ReverseMap();
            CreateMap<FacultyDto, Faculty>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();





            CreateMap<Department, DepartmentDto>()
                  .ForMember(x => x.FacultyName, opt => opt.MapFrom(p => $"{p.Faculty.Name}")).ReverseMap();
            CreateMap<DepartmentDto, Department>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<LanguageDefinitionDto, LanguageDefinition>()
               .ForMember(x => x.CreatedById, opt => opt.Ignore())
               .ForMember(x => x.CreationTime, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<DocumentDto, Document>()
           .ForMember(x => x.CreatedById, opt => opt.Ignore())
           .ForMember(x => x.CreationTime, opt => opt.Ignore())
           .ForMember(x => x.CreatedBy, opt => opt.Ignore())
        .ReverseMap();

            CreateMap<Document, DocumentDto>()
   .ForMember(x => x.Base64, opt => opt.Ignore())
.ReverseMap();


            CreateMap<PermissionDto, Permission>()
                 .ForMember(x => x.CreatedById, opt => opt.Ignore())
                 .ForMember(x => x.CreationTime, opt => opt.Ignore())
                 .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Permission, PermissionDto>()
                .ForMember(x => x.CreatedByFullName, opt => opt.MapFrom(p => $"{p.CreatedBy.Username}"))
           .ReverseMap();

            CreateMap<RoleDto, Role>()
             .ForMember(x => x.CreatedById, opt => opt.Ignore())
             .ForMember(x => x.CreationTime, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();






            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDto, User>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();


            CreateMap<Menu, MenuDto>()
              .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
              .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));

            CreateMap<MenuDto, Menu>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<NewsDto, News>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Announcement, AnnouncementDto>().ReverseMap();
            CreateMap<AnnouncementDto, Announcement>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<ActivityDto, Activity>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<SliderDto, Slider>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();


            CreateMap<Units, UnitsDto>().ReverseMap();
            CreateMap<UnitsDto, Units>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();


            CreateMap<Content, ContentDto>().ReverseMap();
            CreateMap<ContentDto, Content>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<ContentDocument, ContentDocumentDto>().ReverseMap();
            CreateMap<ContentDocumentDto, ContentDocument>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Pages, PagesDto>().ReverseMap();
            CreateMap<PagesDto, Pages>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<LanguageDto, Language>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<LogInfo, LogInfoDto>().ReverseMap();


            CreateMap<Setting, SettingDto>().ReverseMap(); // ters dönüşüm için gerekli

            CreateMap<SettingDto, Setting>()
         .ForMember(x => x.CreatedById, opt => opt.Ignore())
         .ForMember(x => x.CreationTime, opt => opt.Ignore())
         .ForMember(x => x.CreatedBy, opt => opt.Ignore()); // ReserveMap yapmamak gerekiyor.

            #region namevalue Selected Options

            CreateMap<Role, NameValue>()
                   .ForMember(x => x.Name, opt => opt.MapFrom(p => p.Name))
                   .ForMember(x => x.Group, opt => opt.MapFrom(p => p.Code))
                   .ForMember(x => x.Value, opt => opt.MapFrom(p => p.Id));

            CreateMap<Faculty, NameValue>()
                .ForMember(x => x.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(p => p.Id));


            CreateMap<Department, NameValue>()
                .ForMember(x => x.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(p => p.Id))
                .ForMember(x => x.Group, opt => opt.MapFrom(p => p.FacultyId));

            CreateMap<Person, NameValue>()
               .ForMember(x => x.Name, opt => opt.MapFrom(p => $"{p.Name} {p.Surname}"))
               .ForMember(x => x.Value, opt => opt.MapFrom(p => p.Id));

            CreateMap<Language, NameValue>()
           .ForMember(x => x.Name, opt => opt.MapFrom(p => $"{p.Code}"))
           .ForMember(x => x.Value, opt => opt.MapFrom(p => p.Id));


            #endregion

            //--------------------------------------------UI-------------------------------------------------//

            //UI-Activity
            CreateMap<Activity, ActivityDtoUI>().ReverseMap();
            CreateMap<ActivityDtoUI, Activity>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Activity, ActivityListDtoUI>().ReverseMap();
            CreateMap<ActivityListDtoUI, Activity>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            //UI-Announcement
            CreateMap<Announcement, AnnouncementDtoUI>().ReverseMap();
            CreateMap<AnnouncementDtoUI, Announcement>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Announcement, AnnouncementListDtoUI>().ReverseMap();
            CreateMap<AnnouncementListDtoUI, Announcement>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            //UI-News
            CreateMap<News, NewsDtoUI>().ReverseMap();
            CreateMap<NewsDtoUI, News>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<News, NewsListDtoUI>().ReverseMap();
            CreateMap<NewsListDtoUI, News>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();


            //UI-slider
            CreateMap<Slider, SliderDtoUI>().ReverseMap();
            CreateMap<SliderDtoUI, Slider>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Slider, SliderListDtoUI>().ReverseMap();
            CreateMap<SliderListDtoUI, Slider>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ReverseMap();

            // UI-Menu
            CreateMap<Menu, MenuDtoUI>()
                
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ReverseMap();

            CreateMap<MenuDtoUI, Menu>()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ReverseMap();


        }
    }
}
