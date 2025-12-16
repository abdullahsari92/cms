
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using AS.Core;
using AS.Core.Helpers;
using AS.Entities.Base;
using AS.Entities.Entity;
using Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AS.Data
{
    public class EfDbContext : DbContext, IDbContext
    {
          public new DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();


        // public DbSet<User> Users { get; set; }

        // public DbSet<Menu> Menus { get; set; }

        public EfDbContext() : base()
        {

        }

        public EfDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfDbContext).Assembly);

            //IsDeleted durumun kontrolü
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
            
                var isDeletedProperty = entityType.ClrType.GetProperty("IsDeleted");
                if (isDeletedProperty != null)
                {
                  
                    var parameter = Expression.Parameter(entityType.ClrType, "x");
                    var property = Expression.Property(parameter, "IsDeleted");
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }


            //var entityTypeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type => !string.IsNullOrEmpty(type.Namespace) && type.GetInterfaces().Select(x => x.Name).FirstOrDefault() == typeof(IEntityTypeConfiguration<>).Name);

            //foreach (var entityTypeConfiguration in entityTypeConfigurations)
            //{

            //    dynamic configurationInstance = Activator.CreateInstance(entityTypeConfiguration);

            //    modelBuilder.ApplyConfiguration(configurationInstance);

            //}

            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            var userId = UserInfoExtensions.GetUserId();


            string[] entitysNames = { "Language", "Setting", "PermissionMenuLine", "Faculty", "Department", "History", "RolePermissionLine", "Permission", "LogInfo" };

            var objList = ChangeTracker.Entries().Where(t => t.State == EntityState.Added).Select(t => t.Entity).ToList();

            foreach (var obj in objList)
            {
                var entityName = obj.GetType().Name;
                var entitiy = obj as BaseEntity;

                if (!entitysNames.Contains(entityName))
                {
                    Add(new History
                    {
                        Id = Guid.NewGuid(),
                        EntityName = entityName,
                        EntityId = entitiy.Id,
                        Data = obj.ToCreateHistoryAsJson(),
                        EntityState = EntityState.Added.ToString(),
                        TransactionerUserId = userId,
                        TransactionerTime = DateTime.Now
                    });
                }
            }

            foreach (var obj in ChangeTracker.Entries().Where(t => t.State == EntityState.Modified).Select(t => t.Entity).ToList())
            {
                var entityName = obj.GetType().Name;

                var entitiy = obj as BaseEntity;

                bool isyou = (entityName == "User" && entitiy.Id == userId);


                var trensactionId = userId == Guid.Empty ? entitiy.Id: userId;
                if (!entitysNames.Contains(entityName))
                {

                    Add(new History
                    {
                        Id = Guid.NewGuid(),
                        EntityId = entitiy.Id,
                        EntityName = entityName,
                        Data = obj.ToCreateHistoryAsJson(),
                        EntityState = EntityState.Modified.ToString(),
                        TransactionerUserId = trensactionId,
                        TransactionerTime = DateTime.Now
                    });   
                }

            }

            foreach (var obj in ChangeTracker.Entries().Where(t => t.State == EntityState.Deleted).Select(t => t.Entity).ToList())
            {
                var entityName = obj.GetType().Name;
                var entitiy = obj as BaseEntity;

                if (!entitysNames.Contains(entityName))
                {
                    Add(new History
                    {
                        Id = Guid.NewGuid(),
                        EntityName = obj.GetType().Name,
                        EntityId = entitiy.Id,
                        Data = obj.ToCreateHistoryAsJson(),
                        EntityState = EntityState.Deleted.ToString(),
                        TransactionerUserId = userId,
                        TransactionerTime = DateTime.Now
                    });
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
