using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AS.Data.Configurations;

/// <inheritdoc />
/// <summary>
/// Veri tabanı Role tablosu konfigürasyonu
/// </summary>
internal class LogInfoConfiguration : IEntityTypeConfiguration<LogInfo>
{
    public void Configure(EntityTypeBuilder<LogInfo> builder)
    {
        // Tablo adı
        builder.ToTable("LogInfo", "AS");

        builder.HasKey(f => f.Id);


        builder.Property(f => f.ControllerActionName).HasColumnType("varchar(250)");
        builder.Property(f => f.Message).HasColumnType("text");
        builder.Property(f => f.Template).HasColumnType("text");


    }

}