using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            // Tablo adı
            builder.ToTable("News", "AS").HasComment("Haberler Tablosu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(400)").HasComment("Haber Başlık");

            builder.Property(x => x.Summary).IsRequired().HasComment("Haber Kısa Özeti ").HasColumnType("varchar(600)");
            builder.Property(x => x.SeoTitle).IsRequired().HasColumnType("varchar(250)").HasComment("Haber Seo Title");


            builder.Property(x => x.ContentDetails).HasColumnType("text").IsRequired();

            builder.Property(x => x.PublishBeginDate).IsRequired();
                  
            builder.Property(x => x.PublishEndDate).IsRequired();
            builder.Property(x => x.ExternalUrl).IsRequired(false).HasComment("Dış Url"); //Dış Url Gerekmediği Durumlar Olabilir (False)
            builder.Property(x => x.NewsType).IsRequired().HasComment("Haber Tipleri");
            builder.Property(x => x.Keyword).IsRequired().HasComment("Tags").HasColumnType("varchar(150)");
            builder.Property(x => x.DisplayOrder).IsRequired();


            builder.HasOne(x => x.Content).WithMany(y => y.NewsList).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Language).WithMany(y => y.NewsList).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.NewsCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.NewsUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();
        }
    }
}
