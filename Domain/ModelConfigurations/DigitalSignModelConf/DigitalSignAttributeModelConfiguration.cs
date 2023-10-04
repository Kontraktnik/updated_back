using Domain.Models.DigitalSignModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.DigitalSignModelConf;

public class DigitalSignAttributeModelConfiguration : IEntityTypeConfiguration<DigitalSignAttribute>
{
    public void Configure(EntityTypeBuilder<DigitalSignAttribute> builder)
    {
    }
}