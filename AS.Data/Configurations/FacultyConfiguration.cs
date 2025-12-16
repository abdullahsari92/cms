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
internal class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        // Tablo adı
        builder.ToTable("Faculty", "AS");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Code).IsRequired();
        builder.Property(f => f.EducationTime).HasMaxLength(2).IsRequired();


        builder.Property(f => f.IsApproved).IsRequired();
        builder.Property(f => f.CreationTime).IsRequired();
        builder.HasOne(f => f.CreatedBy).WithMany(f => f.FacultiesCreatedBy).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(f => f.UpdatedBy).WithMany(y => y.FacultiesUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);


    builder.Property(f => f.Name).IsRequired().HasColumnType("varchar(512)");

    }
}