using Domain.Models.NotificationModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.NotificationModelConf;

public class PhoneNotificationModelConfiguration : IEntityTypeConfiguration<PhoneNotification>
{
    public void Configure(EntityTypeBuilder<PhoneNotification> builder)
    {
        builder.Property(p => p.Code).HasMaxLength(255).IsRequired();
        builder.Property(p => p.Phone).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.ExpiredAt).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.HasOne<User>(p => p.User).WithMany().HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}