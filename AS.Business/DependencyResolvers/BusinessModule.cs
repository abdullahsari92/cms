using AS.Business;
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
            // =========================
            // CORE - PRIVATE SERVICES
            // =========================
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IPermissionService, PermissionManager>();
            services.AddTransient<IRoleService, RoleManager>();
            services.AddTransient<ILanguageDefinitionService, LanguageDefinitionManager>();

            services.AddScoped<IMenuService, MenuManager>(); // 🔥 MENU (SCOPED)

            services.AddTransient<IAuthService, AuthManager>();
            services.AddTransient<IDepartmentService, DepartmentManager>();
            services.AddTransient<IFacultyService, FacultyManager>();
            services.AddTransient<IPersonService, PersonManager>();
            services.AddTransient<IKPSService, KPSService>();

            services.AddTransient<ISettingService, SettingManager>();
            services.AddTransient<IAccountService, AccountManager>();
            services.AddTransient<IEMailSender, EMailSender>();

            services.AddTransient<IDocumentService, DocumentManager>();
            services.AddTransient<IDashboardService, DashboardManager>();

            // =========================
            // CACHE & REDIS
            // =========================
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IRedisService, RedisService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // =========================
            // CMS - PRIVATE
            // =========================
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

            // =========================
            // PUBLIC UI SERVICES (MENU YOK)
            // =========================
            services.AddTransient<IActivityUIService, ActivityUIManager>();
            services.AddTransient<IAnnouncementUIService, AnnouncementUIManager>();
            services.AddTransient<INewsUIService, NewsUIManager>();
            services.AddTransient<ISliderUIService, SliderUIManager>();
            services.AddTransient<IMenuUIService, MenuUIManager>();


            // =========================
            // FLUENT VALIDATION
            // =========================
            services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssembly(
                    AppDomain.CurrentDomain.GetAssemblies().First()));
        }
    }
}
