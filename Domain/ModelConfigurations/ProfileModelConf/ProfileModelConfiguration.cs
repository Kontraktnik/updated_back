using Domain.Models.ProfileModels;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.ProfileModelConf;

public class ProfileModelConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        //GroupStep
        builder.Property(p => p.StepGroupId).IsRequired();
        builder.HasOne<StepGroup>(p => p.StepGroup).WithMany().HasForeignKey(p => p.StepGroupId)
            .OnDelete(DeleteBehavior.Cascade);
        //Step
        builder.Property(p => p.StepId).IsRequired();
        builder.HasOne<Step>(p => p.Step).WithMany().HasForeignKey(p => p.StepId)
            .OnDelete(DeleteBehavior.Cascade);
        //Survey
        builder.Property(p => p.SurveyId).IsRequired();
        builder.HasOne<Survey>(p => p.Survey).WithMany().HasForeignKey(p => p.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.AreaId).IsRequired();
        builder.HasOne<Area>(p => p.Area).WithMany().HasForeignKey(p => p.AreaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Requested User
        builder.Property(p => p.RequestedUserId).IsRequired();
        builder.HasOne<User>(p => p.RequestedUser).WithMany().HasForeignKey(p => p.RequestedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.RequestedUserIIN).IsRequired();
        builder.Property(p => p.RequestedStatus).IsRequired();
        builder.Property(p => p.RequestedSIGN).IsRequired();
        //Confirmed User
        builder.Property(p => p.ConfirmedUserId).IsRequired();
        builder.HasOne<User>(p => p.ConfirmedUser).WithMany().HasForeignKey(p => p.ConfirmedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.ConfirmedUserIIN).IsRequired();
        builder.Property(p => p.ConfirmedStatus).IsRequired();
        builder.Property(p => p.ConfirmedSIGN).IsRequired();
        //Status
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
        
    }
}