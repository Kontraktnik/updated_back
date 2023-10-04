using Domain.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.SystemModelConf;

public class MedicalStatusModelConfiguration : IEntityTypeConfiguration<MedicalStatus>
{
    public void Configure(EntityTypeBuilder<MedicalStatus> builder)
    {
        builder.Property(p => p.Code).IsRequired();
        builder.Property(p => p.TitleEn).IsRequired();
        builder.Property(p => p.TitleRu).IsRequired();
        builder.Property(p => p.TitleKz).IsRequired();
    }
}