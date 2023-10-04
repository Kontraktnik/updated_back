using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Domain.Models.VacancyModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.VacancyModelConf;

public class VacancyModelConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.HasOne<Position>(p => p.Position).WithMany().HasForeignKey(p => p.PositionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<JobCategory>(p => p.JobCategory).WithMany().HasForeignKey(p => p.JobCategoryId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Area>(p => p.Area).WithMany().HasForeignKey(p => p.AreaId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<ArmyType>(p => p.ArmyType).WithMany().HasForeignKey(p => p.ArmyTypeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<ArmyRegion>(p => p.ArmyRegion).WithMany().HasForeignKey(p => p.ArmyRegionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<SecretLevel>(p => p.SecretLevel).WithMany().HasForeignKey(p => p.SecretLevelId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Qualification>(p => p.Qualification).WithMany().HasForeignKey(p => p.QualificationId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<User>(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.SetNull);

        builder.Property(p => p.City).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.DescriptionRu).IsRequired();
        builder.Property(p => p.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(p => p.UpdatedAt).ValueGeneratedOnAddOrUpdate();



    }
}