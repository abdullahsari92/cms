using AS.Business.EmailMessage;
using AS.Business.Interfaces;
using AS.Business.Interfaces.PublicUI;
using AS.Business.PublicUIManager;
using AS.Business.Validators;
using AS.Core.Utilities.IoC;
using Business.Adapters.Redis;
using Core.Caching;
using Core.Caching.Redis;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AS.Business.DependencyResolvers
{
    public class BusinessModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {

            //Transient her defasında yeni bir instance(örnek) oluşturuyor.
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IPermissionService, PermissionManager>();
            services.AddTransient<IRoleService, RoleManager>();
            services.AddTransient<ILanguageDefinitionService, LanguageDefinitionManager>();

            services.AddTransient<IMenuService, MenuManager>();
            services.AddTransient<IAuthService, AuthManager>();
            services.AddTransient<IDepartmentService, DepartmentManager>();
            services.AddTransient<IFacultyService, FacultyManager>();

            services.AddTransient<IPersonService, PersonManager>();


            services.AddTransient<IKPSService, KPSService>();

            services.AddTransient<IEMailSender, EMailSender>();
            services.AddTransient<ISettingService, SettingManager>();
            services.AddTransient<IAccountService, AccountManager>();

            services.AddTransient<IDocumentService, DocumentManager>();
            services.AddTransient<IDashboardService, DashboardManager>();


            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IRedisService, RedisService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //CMS-private 
            services.AddTransient<IContentService, ContentManager>();
            services.AddTransient<INewsService, NewsManager>();
            services.AddTransient<IAnnouncementService, AnnouncementManager>();
            services.AddTransient<IActivityService, ActivityManager>();
            services.AddTransient<IUnitsService, UnitsManager>();
            services.AddTransient<ILanguageService, LanguageManager>();
            services.AddTransient<IPagesService, PagesManager>();
            services.AddTransient<IContentDocumentService, ContentDocumentManager>();
            services.AddTransient<ISliderService, SliderManager>();
            services.AddTransient<ILogInfoService, LogInfoManager>();

            //UI-public
            services.AddTransient<IActivityUIService, ActivityUIManager>();
            services.AddTransient<IAnnouncementUIService, AnnouncementUIManager>();
            services.AddTransient<INewsUIService, NewsUIManager>();
            services.AddTransient<ISliderUIService, SliderUIManager>();


            //FluentValidation
            services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().First()));
        }


    }
}

