
using AS.Core;
using AS.Core.Utilities.IoC;
using AS.Entities.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace AS.Data.DependencyResolvers
{
    public class DataAccessModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            //Transient her defasında yeni bir instance(örnek) oluşturuyor.
            
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Permission>, Repository<Permission>>();
            services.AddScoped<IRepository<RolePermissionLine>, Repository<RolePermissionLine>>();


            services.AddScoped<IRepository<RoleUserLine>, Repository<RoleUserLine>>();

            services.AddScoped<IRepository<Role>, Repository<Role>>();

            services.AddScoped<IRepository<LanguageDefinition>, Repository<LanguageDefinition>>();
            services.AddScoped<IRepository<Menu>, Repository<Menu>>();
            services.AddScoped<IRepository<Person>, Repository<Person>>();

            services.AddScoped<IRepository<Department>, Repository<Department>>();
            services.AddScoped<IRepository<Faculty>, Repository<Faculty>>();

            services.AddScoped<IRepository<Setting>, Repository<Setting>>();
            services.AddScoped<IRepository<Document>, Repository<Document>>();

            //CMS
            services.AddScoped<IRepository<News>, Repository<News>>();
            services.AddScoped<IRepository<Announcement>, Repository<Announcement>>();
            services.AddScoped<IRepository<Activity>, Repository<Activity>>();
            services.AddScoped<IRepository<Content>, Repository<Content>>();
            services.AddScoped<IRepository<Units>, Repository<Units>>();
            services.AddScoped<IRepository<Pages>, Repository<Pages>>();
            services.AddScoped<IRepository<Language>, Repository<Language>>();
            services.AddScoped<IRepository<ContentDocument>, Repository<ContentDocument>>();
            services.AddScoped<IRepository<Slider>, Repository<Slider>>();
      

   

        }
    }
}
