using Domain.Models.DigitalSignModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.DigitalSignModelConf;

public class DigitalSignInfoModelConfiguration : IEntityTypeConfiguration<DigitalSignInfo>
{
    public void Configure(EntityTypeBuilder<DigitalSignInfo> builder)
    {
    }
}