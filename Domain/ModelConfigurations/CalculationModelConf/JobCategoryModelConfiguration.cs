using Domain.Models.CalculationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.CalculationModelConf;

public class JobCategoryModelConfiguration : IEntityTypeConfiguration<JobCategory>
{
    public void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        builder.Property(p => p.TitleEn).HasMaxLength(255).IsRequired();
        builder.Property(p => p.TitleRu).HasMaxLength(255).IsRequired();
        builder.Property(p => p.TitleKz).HasMaxLength(255).IsRequired();
    }
}