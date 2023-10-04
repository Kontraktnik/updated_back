using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations;

public class RoleModelConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(p => p.TitleEn).IsRequired();
        builder.Property(p => p.TitleRu).IsRequired();
        builder.Property(p => p.TitleKz).IsRequired();
    }
}