using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.CalculationModelConf;

public class PositionModelConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.Property(p => p.TitleEn).HasMaxLength(255).IsRequired();
        builder.Property(p => p.TitleRu).HasMaxLength(255).IsRequired();
        builder.Property(p => p.TitleKz).HasMaxLength(255).IsRequired();

        builder.HasOne<JobCategory>(p => p.JobCategory)
            .WithMany().HasForeignKey(p => p.JobCategoryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<SecretLevel>(p => p.SecretLevel)
            .WithMany().HasForeignKey(p => p.SecretLevelId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<ArmyType>(p => p.ArmyType)
            .WithMany().HasForeignKey(p => p.ArmyTypeId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne<CategoryPosition>(p => p.CategoryPosition)
            .WithMany().HasForeignKey(p => p.CategoryPositionId).OnDelete(DeleteBehavior.SetNull);
        
    }
}