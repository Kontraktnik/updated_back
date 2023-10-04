using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SystemModelConf;

public class RelativeModelConfiguration : IEntityTypeConfiguration<Relative>
{
    public void Configure(EntityTypeBuilder<Relative> builder)
    {
        builder.Property(p => p.TitleEn).IsRequired();
        builder.Property(p => p.TitleRu).IsRequired();
        builder.Property(p => p.TitleKz).IsRequired();
    }
}