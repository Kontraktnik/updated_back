using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SurveyModelConf;

public class SurveyDriverModelConfiguration : IEntityTypeConfiguration<SurveyDriver>
{
    public void Configure(EntityTypeBuilder<SurveyDriver> builder)
    {
        builder.Property(p => p.SurveyId).IsRequired();
        builder.HasOne<Survey>(p => p.Survey).WithMany().HasForeignKey(p => p.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.DriverLicenseId).IsRequired();
        builder.HasOne<DriverLicense>(p => p.DriverLicense).WithMany().HasForeignKey(p => p.DriverLicenseId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}