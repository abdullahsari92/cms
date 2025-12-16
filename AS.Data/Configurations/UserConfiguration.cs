using AS.Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS.Data.Configurations;

/// <inheritdoc />
/// <summary>
/// Veri tabaný User tablosu konfigürasyonu
/// </summary>
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Tablo adý
        builder.ToTable("Users", "AS");

        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.IsApproved).IsRequired();
        builder.Property(x => x.CreationTime).IsRequired();
        builder.Property(x => x.UpdateTime);
        builder.Property(x => x.UserType).HasMaxLength(1);




    builder.HasOne(p => p.CreatedBy).WithMany(t => t.UsersCreatedBy).HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UpdatedBy).WithMany(y => y.UsersUpdatedBy).IsRequired(false).HasForeignKey(x => x.UpdatedById).OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Username).IsRequired().HasColumnType("varchar(512)");
        builder.HasIndex(x => x.Username).IsUnique().HasName("UK_UserUsername");
        builder.Property(x => x.Password).IsRequired().HasColumnType("char(128)");
        builder.Property(x => x.Email).IsRequired().HasColumnType("varchar(512)");
        builder.HasIndex(x => x.Email).IsUnique().HasName("UK_UserEmail");


    }
}