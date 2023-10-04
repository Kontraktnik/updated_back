using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SystemModelConf;

public class ArmyRankModelConfiguration : IEntityTypeConfiguration<ArmyRank>
{
    public void Configure(EntityTypeBuilder<ArmyRank> builder)
    {
        builder.Property(p => p.TitleEn).IsRequired();
        builder.Property(p => p.TitleRu).IsRequired();
        builder.Property(p => p.TitleKz).IsRequired();
    }
}