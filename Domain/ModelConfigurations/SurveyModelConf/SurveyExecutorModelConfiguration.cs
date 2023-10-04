using Domain.Models.SurveyModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SurveyModelConf;

public class SurveyExecutorModelConfiguration : IEntityTypeConfiguration<SurveyExecutor>
{
    public void Configure(EntityTypeBuilder<SurveyExecutor> builder)
    {
        builder.Property(p => p.SurveyId).IsRequired();
        builder.HasOne<Survey>(p => p.Survey).WithMany().HasForeignKey(p => p.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.ExecutorId).IsRequired();
        builder.HasOne<User>(p => p.Executor).WithMany().HasForeignKey(p => p.ExecutorId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.DirectorId).IsRequired();
        builder.HasOne<User>(p => p.Director).WithMany().HasForeignKey(p => p.DirectorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}