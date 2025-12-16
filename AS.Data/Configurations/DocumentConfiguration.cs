using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Document = AS.Entities.Entity.Document;

namespace AS.Data.Configurations;

/// <inheritdoc />
/// <summary>
/// Veri tabanı Role tablosu konfigürasyonu
/// </summary>
internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        //// Tablo adı
        builder.ToTable("Document", "AS");

        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(256)");

        builder.Property(x => x.Path).IsRequired().HasColumnType("varchar(400)");
        builder.Property(x => x.DocumentType).HasColumnType("varchar(50)").HasComment("Dokümanın hangi modüle eklendiği bilgisini kategorize etmek için kullanılmıştır.");

        builder.Property(x => x.Description).IsRequired(false).HasColumnType("varchar(500)");

        builder.Property(x => x.Extension).IsRequired().HasColumnType("varchar(100)");
        builder.Property(x => x.Keyword).IsRequired(false).HasColumnType("varchar(150)");

        builder.Property(x => x.DocLength).IsRequired();


        builder.Property(x => x.CreationTime).IsRequired();
        builder.HasOne(x => x.CreatedBy).WithMany(y => y.DocumentsCreatedBy).OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.UpdateTime);
        builder.HasOne(x => x.UpdatedBy).WithMany(y => y.DocumentsUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.IsApproved).IsRequired();

    }
}