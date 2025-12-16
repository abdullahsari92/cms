using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Data.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            // Tablo adı
            builder.ToTable("Content", "AS").HasComment("İçerik Tablosu");

            builder.HasKey(x => x.Id);


            // UnitId ilişkisi
            builder.Property(x => x.UnitId).IsRequired().HasComment("Birim Id");
            builder.HasOne(x => x.Units).WithMany(y => y.Contents).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ContentCategory).IsRequired().HasComment("İçerik Kategorisi : News=0,       Activity=1,      Announcement=2, Slider=3");
    
            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.ContentCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.ContentUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();
        }
    }
}
