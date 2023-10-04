using Domain.Models.DigitalSignModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.DigitalSignModelConf;

public class DigitalSignBinaryModelConfiguration : IEntityTypeConfiguration<DigitalSignBinary>
{
    public void Configure(EntityTypeBuilder<DigitalSignBinary> builder)
    {
    }
}