using Domain.Models.StepModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.StepModelConf;

public class StepOrderModelConfiguration : IEntityTypeConfiguration<StepOrder>
{
    public void Configure(EntityTypeBuilder<StepOrder> builder)
    {
        builder.Property(p => p.StepId).IsRequired();
        builder.HasOne<Step>(p => p.Step).WithMany().HasForeignKey(p => p.StepId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(p => p.PreviousStepId).IsRequired();
        builder.HasOne<Step>(p => p.PreviousStep).WithMany().HasForeignKey(p => p.PreviousStepId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Property(p => p.NextStepId).IsRequired();
        builder.HasOne<Step>(p => p.NextStep).WithMany().HasForeignKey(p => p.NextStep)
            .OnDelete(DeleteBehavior.SetNull);
    }
}