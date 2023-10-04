using Domain.Models.StepModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.StepModelConf;

public class StepGroupModelConfiguration : IEntityTypeConfiguration<StepGroup>
{
    public void Configure(EntityTypeBuilder<StepGroup> builder)
    {
        builder.Property(p => p.Order).IsRequired();
        builder.Property(p => p.TitleEn).IsRequired();
        builder.Property(p => p.TitleRu).IsRequired();
        builder.Property(p => p.TitleKz).IsRequired();
    }
}