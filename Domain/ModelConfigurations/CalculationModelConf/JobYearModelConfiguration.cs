using Domain.Models.CalculationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.CalculationModelConf;

public class JobYearModelConfiguration : IEntityTypeConfiguration<JobYear>
{
    public void Configure(EntityTypeBuilder<JobYear> builder)
    {
        builder.HasOne<JobCategory>(p => p.JobCategory)
            .WithMany().HasForeignKey(p => p.JobCategoryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<ServiceYear>(p => p.ServiceYear)
            .WithMany().HasForeignKey(p => p.ServiceYearId).OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.Salary).IsRequired();
    }
}