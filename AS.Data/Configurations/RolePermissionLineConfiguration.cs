using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations
{

    /// <inheritdoc />
    /// <summary>
    /// Veri tabaný RolePermissionLine tablosu konfigürasyonu
    /// </summary>
    internal class RolePermissionLineConfiguration : IEntityTypeConfiguration<RolePermissionLine>
    {
        public void Configure(EntityTypeBuilder<RolePermissionLine> builder)
        {
            builder.ToTable("RolePermissionLines", "AS");

            builder.HasKey(x => x.Id);
           // builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.RolePermissionLinesCreatedBy).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.RolePermissionLinesUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Role).WithMany(y => y.RolePermissionLines).IsRequired().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Permission).WithMany(y => y.RolePermissionLines).IsRequired(false).HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
