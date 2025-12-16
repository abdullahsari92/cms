using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations;

/// <inheritdoc />
/// <summary>
/// Veri tabaný RoleUserLine tablosu konfigürasyonu
/// </summary>
internal class RoleUserLineConfiguration : IEntityTypeConfiguration<RoleUserLine>
{
    public void Configure(EntityTypeBuilder<RoleUserLine> builder)
    {
        builder.ToTable("RoleUserLines", "AS");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreationTime).IsRequired();
        builder.Property(x => x.UpdateTime);

        builder.HasOne(f => f.Units).WithMany(y => y.RoleUserLineList).HasForeignKey(p => p.UnitId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

        builder.HasOne(x => x.Role).WithMany(y => y.RoleUserLines).HasForeignKey(p=>p.RoleId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.User).WithMany(y => y.RoleUserLines).IsRequired().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CreatedBy).WithMany(t => t.RoleUserLinesCreatedBy).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UpdatedBy).WithMany(y => y.RoleUserLinesUpdatedBy).HasForeignKey(x => x.UpdatedById).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
 

    }
}