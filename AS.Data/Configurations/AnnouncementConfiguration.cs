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
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            // Tablo adı
            builder.ToTable("Announcement", "AS").HasComment("Duyurular Tablosu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(400)").HasComment("Duyuru Başlık");
            builder.Property(x => x.ContentDetail).IsRequired().HasComment("Duyuru İçeriği ").HasColumnType("varchar(4000)");
            builder.Property(x => x.SeoTitle).IsRequired().HasColumnType("varchar(500)").HasComment("Duyuru Seo Title");
            builder.Property(x => x.PublishBeginDate).IsRequired();
            builder.Property(x => x.PublishEndDate).IsRequired();
            builder.Property(x => x.AnnouncementType).IsRequired().HasComment("Duyuru Tipleri");
            builder.Property(x => x.DisplayOrder).IsRequired();


            builder.HasOne(x => x.Content).WithMany(y => y.AnnouncementList).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Language).WithMany(y => y.AnnouncementList).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.AnnouncementCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.AnnouncementUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();
        }
    }
}
