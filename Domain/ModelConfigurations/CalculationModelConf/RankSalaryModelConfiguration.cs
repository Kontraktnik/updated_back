using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.CalculationModelConf;

public class RankSalaryModelConfiguration : IEntityTypeConfiguration<RankSalary>
{
    public void Configure(EntityTypeBuilder<RankSalary> builder)
    {
        builder.HasOne<ArmyRank>(p => p.ArmyRank)
            .WithMany().HasForeignKey(p => p.ArmyRankId).OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.Salary).IsRequired();

    }
}