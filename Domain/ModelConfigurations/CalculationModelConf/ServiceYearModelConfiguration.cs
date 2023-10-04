using Domain.Models.CalculationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.CalculationModelConf;

public class ServiceYearModelConfiguration : IEntityTypeConfiguration<ServiceYear>
{
    public void Configure(EntityTypeBuilder<ServiceYear> builder)
    {
        builder.Property(p => p.TitleEn).HasMaxLength(255).IsRequired();
        builder.Property(p => p.TitleRu).HasMaxLength(255).IsRequired();
        builder.Property(p => p.TitleKz).HasMaxLength(255).IsRequired();
        builder.Property(p => p.Min).IsRequired();
        builder.Property(p => p.Max).IsRequired();
    }
}