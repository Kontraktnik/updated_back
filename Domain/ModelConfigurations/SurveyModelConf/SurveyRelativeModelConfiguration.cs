using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SurveyModelConf;

public class SurveyRelativeModelConfiguration : IEntityTypeConfiguration<SurveyRelative>
{
    public void Configure(EntityTypeBuilder<SurveyRelative> builder)
    {
        builder.Property(p => p.SurveyId).IsRequired();
        builder.HasOne<Survey>(p => p.Survey).WithMany().HasForeignKey(p => p.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.RelativeId).IsRequired();
        builder.HasOne<Relative>(p => p.Relative).WithMany().HasForeignKey(p => p.RelativeId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.SurName).IsRequired();
        builder.Property(p => p.IIN).IsRequired();
        builder.Property(p => p.BirthDate).IsRequired();


        
    }
}