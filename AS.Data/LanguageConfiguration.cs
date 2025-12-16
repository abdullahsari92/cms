using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Data
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            // Tablo adı
            builder.ToTable("Language", "AS").HasComment("Dil Tablosu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(400)").HasComment("Dil Adı");


            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.LanguageCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.LanguageUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();
        }
    }
}

