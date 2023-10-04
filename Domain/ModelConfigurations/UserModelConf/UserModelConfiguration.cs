using Domain.Models.NotificationModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations;

public class UserModelConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.IIN).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
        builder.Property(p => p.Surname).HasMaxLength(255).IsRequired();
        builder.Property(p => p.FullName).IsRequired();
        builder.Property(p => p.Phone).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Password).IsRequired();
        builder.Property(p => p.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
        builder.Property(p => p.Verified).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        //ForeignKey
        builder.HasOne<Role>(p => p.Role).WithMany().HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Area?>(p => p.Area).WithMany().HasForeignKey(p => p.AreaId).OnDelete(DeleteBehavior.SetNull);

        
    }
}