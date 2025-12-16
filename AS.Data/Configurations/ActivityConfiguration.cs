using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AS.Entities.Entity;

namespace AS.Data.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            //Tablo Adı
            builder.ToTable("Activity", "AS").HasComment("Etkinlikler Tablosu");


            builder.HasKey(x => x.Id);

            //Property 
            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(400)").HasComment("Etkinlik Başlık");
            builder.Property(x => x.ContentDetail).IsRequired().HasComment("Etkinlik İçeriği ").HasColumnType("varchar(850)");
            builder.Property(x => x.SeoTitle).IsRequired().HasColumnType("varchar(250)").HasComment("Etkinlik Seo Title");
            builder.Property(x => x.Speakers).IsRequired().HasColumnType("varchar(250)").HasComment("Etkinlik Konuşmacıları");
            builder.Property(x => x.Moderator).IsRequired().HasColumnType("varchar(250)").HasComment("Etkinlik Moderatörleri");
            builder.Property(x => x.Responsible).IsRequired().HasColumnType("varchar(250)").HasComment("Etkinlik Sorumluları");
            builder.Property(x => x.PosterUrl).IsRequired().HasColumnType("varchar(250)").HasComment("Etkinlik Poster Url");
            builder.Property(x => x.ActivityCategory).IsRequired().HasColumnType("varchar(250)").HasComment("Etkinlik Kategorisi(   Seminar = 0,  Webinar = 1,  Meeting = 2,  Workshop = 3,  Training = 4,)");
            builder.Property(x => x.ActivityType).IsRequired().HasColumnType("varchar(250)").HasComment("Etkinlik Tipi (Physical = 1, Virtual = 2, Hybrid = 3,)");
            builder.Property(x => x.PublishBeginDate).IsRequired().HasComment("Etkinliğin sitede yayına alınma başlangıç tarihi");
            builder.Property(x => x.PublishEndDate).IsRequired().HasComment("Etkinliğin sitede yayından kaldırılma tarihi");
            builder.Property(x => x.ActivityStartDate).IsRequired().HasComment("Etkinliğin gerçekleşme başlangıç tarihi");
            builder.Property(x => x.ActivityEndDate).IsRequired().HasComment("Etkinliğin gerçekleşme bitiş tarihi");


            //Relationships
            builder.HasOne(x => x.Content).WithMany(y => y.ActivityList).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Language).WithMany(y => y.ActivityList).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.ActivityCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.ActivityUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();

        }
    }
}
