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
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.ToTable("Slider", "AS").HasComment("Slider Tablosu");

            builder.HasKey(x => x.Id);

            //Property
            builder.Property(x=>x.Title).IsRequired().HasComment("Slider Başlık").HasColumnType("varchar(250)");
            builder.Property(x => x.Description).IsRequired().HasComment("Slider Açıklaması").HasColumnType("varchar(700)");
            builder.Property(x => x.DisplayOrder).IsRequired().HasComment("Slider Sıralaması");

            //Relationships
            builder.HasOne(x => x.Document).WithMany(y => y.SliderList).HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Units).WithMany(y => y.SliderList).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Language).WithMany(y => y.SliderList).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.SliderCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.SliderUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();
        }
    }
}
