using Domain.Models.CalculationModels;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Domain.Models.VacancyModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SurveyModelConf;

public class SurveyModelConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.Property(p => p.UserId).IsRequired();
        builder.HasOne<User>(p => p.User).WithMany().HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.IIN).IsRequired();
        builder.Property(p => p.FullName).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Phone).IsRequired();

        builder.Property(p => p.BirthAreaId).IsRequired();
        builder.HasOne<Area>(p => p.BirthArea).WithMany().HasForeignKey(p => p.BirthAreaId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.Region).IsRequired();
        builder.Property(p => p.City).IsRequired();
        builder.Property(p => p.Street).IsRequired();
    
        builder.Property(p => p.EducationId).IsRequired();
        builder.HasOne<Education>(p => p.Education).WithMany().HasForeignKey(p => p.EducationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.Experienced).IsRequired();
        builder.Property(p => p.Served).IsRequired();
        
        builder.HasOne<ArmyRank>(p => p.ArmyRank).WithMany().HasForeignKey(p => p.ArmyRankId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Property(p => p.VTShServed).IsRequired();
        builder.HasOne<VTSh>(p => p.VTSh).WithMany().HasForeignKey(p => p.VTShId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(p => p.PositionId).IsRequired();
        builder.HasOne<Position>(p => p.Position).WithMany().HasForeignKey(p => p.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.ContractYear).IsRequired();
        builder.Property(p => p.AreaId).IsRequired();
        builder.HasOne<Area>(p => p.Area).WithMany().HasForeignKey(p => p.AreaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Vacancy>(p => p.Vacancy).WithMany().HasForeignKey(p => p.VacancyId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(p => p.ImageUrl).IsRequired();
        builder.Property(p => p.AutoBiography).IsRequired();
        builder.Property(p => p.EducationUrl).IsRequired();
        builder.Property(p => p.IncomePropertyUrl).IsRequired();
        builder.Property(p => p.EmploymentUrl).IsRequired();
        builder.Property(p => p.MillitaryUrl).IsRequired();
        builder.Property(p => p.SpecialCheckUrl).IsRequired();
        builder.Property(p => p.IdentityCardUrl).IsRequired();
        builder.Property(p => p.Agreed).IsRequired();
        builder.Property(p => p.SignKey).IsRequired();
        builder.Property(p => p.Status).IsRequired();

        builder.HasOne<StepGroup>(p => p.StepGroup).WithMany().HasForeignKey(p => p.StepGroupId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne<Step>(p => p.CurrentStep).WithMany().HasForeignKey(p => p.CurrentStepId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne<MedicalStatus>(p => p.MedicalStatus).WithMany().HasForeignKey(p => p.MedicalStatusId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany<SurveyRelative>(p => p.SurveyRelatives);
        builder.HasMany<SurveyDriver>(p => p.SurveyDrivers);

        builder.Property(p => p.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();

    }
}