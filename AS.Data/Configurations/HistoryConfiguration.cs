using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace AS.Data.Configurations;

/// <inheritdoc />
/// <summary>
/// Veri tabanı Role tablosu konfigürasyonu
/// </summary>
internal class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        // Tablo adı
        builder.ToTable("History", "AS");

        builder.HasKey(f => f.Id);


        builder.Property(f => f.EntityName).HasColumnType("varchar(250)");
        builder.Property(f => f.EntityState).HasColumnType("varchar(100)");

        builder.Property(f => f.EntityId).IsRequired(false);
        
        builder.Property(f => f.Data).HasColumnType("text");

        builder.HasOne(p => p.TransactionerUser).WithMany(t => t.HistoryTransactionerUser).HasForeignKey(x => x.TransactionerUserId).OnDelete(DeleteBehavior.Restrict);


    }

}