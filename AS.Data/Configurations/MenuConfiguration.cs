using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations;

/// <inheritdoc />
/// <summary>
/// Veri tabanı Menu tablosu konfigürasyonu
/// </summary>
internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {

        builder.ToTable("Menus", "AS");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(512)");
        builder.Property(x => x.Url).IsRequired(false).HasColumnType("varchar(512)");
        builder.Property(x => x.Icon).IsRequired(false).HasColumnType("varchar(512)");
        builder.Property(x => x.Description).IsRequired(false).HasColumnType("varchar(512)");
        builder.Property(x => x.DisplayOrder).IsRequired();
        builder.Property(x => x.IsApproved).IsRequired();
        builder.Property(x => x.CreationTime).IsRequired();
        builder.Property(x => x.ParentId).IsRequired(false);

        builder.HasOne(x => x.Units) .WithMany(y=>y.Menus).HasForeignKey(x => x.UnitId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Pages).WithOne().HasForeignKey<Menu>(p => p.PageId).IsRequired(false);

        builder.HasOne(x => x.Language).WithMany(y => y.MenuList).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.CreatedBy).WithMany(t => t.MenusCreatedBy).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UpdatedBy).WithMany(y => y.MenusUpdatedBy).IsRequired(false).HasForeignKey(x => x.UpdatedById).OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Parent).WithMany(y => y.Children).HasForeignKey(x => x.ParentId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
    }
}