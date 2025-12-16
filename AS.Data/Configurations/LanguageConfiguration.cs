using AS.Entities.Dtos;
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
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Language", "AS");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.SeoCode).IsRequired(false).HasColumnType("varchar(100)");
            builder.Property(x => x.Code).IsRequired(false).HasColumnType("varchar(100)");

         
            builder.HasMany(x => x.NewsList).WithOne(x => x.Language).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(x => x.PagesList).WithOne(x => x.Language).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.MenuList).WithOne(x => x.Language).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(p => p.CreatedBy).WithMany(t => t.LanguageCreatedBy).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.LanguageUpdatedBy).IsRequired(false).HasForeignKey(x => x.UpdatedById).OnDelete(DeleteBehavior.Restrict);


    
        }
    }
}
