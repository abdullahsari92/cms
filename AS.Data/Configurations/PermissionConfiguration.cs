using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations
{

    /// <inheritdoc />
    /// <summary>
    /// Veri tabanı Permission tablosu konfigürasyonu
    /// </summary>
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            // Tablo adı
            builder.ToTable("Permissions", "AS");

            builder.HasKey(x => x.Id);
           // builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.IsApproved).IsRequired();
            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.PermissionsCreatedBy).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.PermissionsUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(512)");
            builder.Property(x => x.Description).HasColumnType("varchar(512)");
            builder.Property(x => x.ControllerName).IsRequired().HasColumnType("varchar(512)");
            builder.Property(x => x.ActionName).IsRequired().HasColumnType("varchar(512)");
        }
    }
}