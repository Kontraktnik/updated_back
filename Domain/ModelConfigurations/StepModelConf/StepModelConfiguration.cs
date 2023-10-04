using Domain.Models.StepModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.StepModelConf;

public class StepModelConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure(EntityTypeBuilder<Step> builder)
    {
        builder.Property(p => p.TitleEn).IsRequired();
        builder.Property(p => p.TitleRu).IsRequired();
        builder.Property(p => p.TitleKz).IsRequired();
        builder.Property(p => p.IsFirst).IsRequired();
        builder.Property(p => p.IsLast).IsRequired();
        builder.Property(p => p.DayLimit).IsRequired();
        
        builder.Property(p => p.RequestedRoleId).IsRequired();
        builder.HasOne<Role>(p => p.RequestedRole).WithMany().HasForeignKey(p => p.RequestedRoleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.ConfirmedRoleId).IsRequired();
        builder.HasOne<Role>(p => p.ConfirmedRole).WithMany().HasForeignKey(p => p.ConfirmedRoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}