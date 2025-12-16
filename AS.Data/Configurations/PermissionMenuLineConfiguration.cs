using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations
{

    /// <inheritdoc />
    /// <summary>
    /// Veri tabaný PermissionMenuLine tablosu konfigürasyonu
    /// </summary>
    internal class PermissionMenuLineConfiguration : IEntityTypeConfiguration<PermissionMenuLine>
    {
        public void Configure(EntityTypeBuilder<PermissionMenuLine> builder)
        {
            builder.ToTable("PermissionMenuLines", "AS");

            builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.PermissionMenuLinesCreatedBy).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.PermissionMenuLinesUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Permission).WithMany(y => y.PermissionMenuLines).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Menu).WithMany(y => y.PermissionMenuLines).IsRequired().OnDelete(DeleteBehavior.Restrict);


        }
    }
}
