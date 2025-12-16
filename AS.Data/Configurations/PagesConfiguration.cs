using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AS.Data.Configurations
{
    public class PagesConfiguration : IEntityTypeConfiguration<Pages>
    {
        public void Configure(EntityTypeBuilder<Pages> builder)
        {
            // Tablo adı
            builder.ToTable("Pages", "AS").HasComment("Sayfalar Tablosu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(400)").HasComment("Sayfa Başlık");
            builder.Property(x => x.SeoTitle).IsRequired().HasColumnType("varchar(150)").HasComment("Seo");
            builder.Property(x => x.ContentDetail).HasColumnType("text").IsRequired();
            builder.Property(x => x.ExternalUrl).IsRequired(false);
                 
            builder.Property(x => x.Keyword).IsRequired().HasComment("Tags").HasColumnType("varchar(150)");


            //one to one 
            builder.HasOne(m => m.Menu).WithOne().HasForeignKey<Pages>(p => p.MenuId) .IsRequired(false);

            builder.HasOne(p => p.Content).WithMany(p=>p.PagesList).HasForeignKey(p => p.ContentId).IsRequired();
            builder.HasOne(x => x.Language).WithMany(y => y.PagesList).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.PagesCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.PagesUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();
        }
    }
}
