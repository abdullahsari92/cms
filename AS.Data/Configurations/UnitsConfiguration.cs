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
    public class UnitsConfiguration : IEntityTypeConfiguration<Units>
    {
        public void Configure(EntityTypeBuilder<Units> builder)
        {
            // Tablo adı
            builder.ToTable("Units", "AS").HasComment("Birimler Tablosu");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(400)").HasComment("Birim Adı");

            builder.Property(x => x.Email).IsRequired().HasComment("Birim Email ").HasColumnType("varchar(100)");
            builder.Property(x => x.Phone).IsRequired().HasColumnType("varchar(25)").HasComment("Birim Telefon");


            builder.Property(x => x.Url).IsRequired().HasColumnType("varchar(350)").HasComment("Birim Url");

            builder.Property(x => x.Address).IsRequired().HasColumnType("varchar(400)").HasComment("Birim Adresi");

            builder.Property(x => x.Fax).IsRequired().HasColumnType("varchar(100)").HasComment("Birim Fax");
            builder.Property(x => x.WhatshappNumber).HasColumnType("varchar(20)").IsRequired(false);
            builder.Property(x => x.LogoDocumentId).IsRequired().HasComment("Birim Logo 1");
            builder.Property(x => x.LogoTwoDocumentId).IsRequired(false).HasComment("Birim Logo 2");
            builder.Property(x => x.Description).IsRequired().HasColumnType("varchar(700)").HasComment("Birim Açıklaması");
            builder.Property(x => x.Theme).IsRequired().HasColumnType("varchar(400)").HasComment("Birim Teması");
            builder.Property(x => x.Color).HasColumnType("varchar(40)").HasComment("Birim Tema Rengi");


            builder.HasOne(f => f.LogoDocument).WithMany(f => f.UnitsLogoDocumentList).HasForeignKey(p => p.LogoDocumentId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(f => f.LogoTwoDocument).WithMany(f => f.UnitsLogoTwoDocumentList).HasForeignKey(p => p.LogoTwoDocumentId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.UnitsCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.UnitsUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();
        }
    }
}
