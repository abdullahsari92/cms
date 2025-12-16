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
internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        // Tablo adı
        builder.ToTable("Department", "AS");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Code).IsRequired();
        builder.Property(f => f.Name).IsRequired().HasColumnType("varchar(512)");


        builder.Property(f => f.IsApproved).IsRequired();
        builder.Property(f => f.CreationTime).IsRequired();

        builder.HasOne(f => f.Faculty).WithMany(y => y.DepartmentList).IsRequired().HasForeignKey(f => f.FacultyId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.CreatedBy).WithMany(f => f.DepartmentsCreatedBy).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(f => f.UpdatedBy).WithMany(y => y.DepartmentsUpdatedBy).IsRequired(false).OnDelete(DeleteBehavior.Restrict);



    }
}