using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations;

/// <inheritdoc />
/// <summary>
/// Veri tabanı Role tablosu konfigürasyonu
/// </summary>
internal class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        // Tablo adı
        builder.ToTable("Setting", "AS");

        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.IsApproved).IsRequired();
        builder.Property(x => x.CreationTime).IsRequired();
        builder.HasOne(x => x.CreatedBy).WithMany(y => y.SettingCreatedBy).OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.UpdateTime);
        builder.HasOne(x => x.UpdatedBy).WithMany(y => y.SettingUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Code).IsRequired().HasColumnType("varchar(512)");
        builder.Property(x => x.Value).IsRequired().HasColumnType("varchar(512)");
        builder.Property(x => x.Description).HasColumnType("varchar(512)");

    }
}