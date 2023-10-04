using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SystemModelConf;

public class VTShModelConfiguration : IEntityTypeConfiguration<VTSh>
{
    public void Configure(EntityTypeBuilder<VTSh> builder)
    {
        builder.Property(p => p.TitleEn).IsRequired();
        builder.Property(p => p.TitleRu).IsRequired();
        builder.Property(p => p.TitleKz).IsRequired();
    }
}