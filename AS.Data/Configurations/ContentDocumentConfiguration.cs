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
    public class ContentDocumentConfiguration : IEntityTypeConfiguration<ContentDocument>
    {
        public void Configure(EntityTypeBuilder<ContentDocument> builder)
        {
            // Tablo adı
            builder.ToTable("ContentDocument", "AS").HasComment("İçerik Doküman Tablosu");

            builder.HasKey(x => x.Id);

            builder.HasOne(cd => cd.Content).WithMany(c => c.ContentDocuments).HasForeignKey(cd => cd.ContentId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(cd => cd.Document).WithMany(d => d.ContentDocuments).HasForeignKey(cd => cd.DocumentId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreationTime).IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany(y => y.ContentDocumentCreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.UpdateTime);
            builder.HasOne(x => x.UpdatedBy).WithMany(y => y.ContentDocumentUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.IsApproved).IsRequired();

        }
    }
}
