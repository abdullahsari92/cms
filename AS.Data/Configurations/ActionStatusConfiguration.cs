using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations
{

    /// <inheritdoc />
    /// <summary>
    /// Veri tabanı Permission tablosu konfigürasyonu
    /// </summary>
    internal class ActionStatusConfiguration : IEntityTypeConfiguration<ActionStatus>
    {
        public void Configure(EntityTypeBuilder<ActionStatus> builder)
        {
            // Tablo adı
            builder.ToTable("ActionStatus", "AS");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description);


        }
    }
}