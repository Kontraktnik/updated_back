using System.Net.Mime;
using Domain.Models.ProfileModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.ProfileModelConf;

public class ProfileFileModelConfiguration : IEntityTypeConfiguration<ProfileFile>
{
    public void Configure(EntityTypeBuilder<ProfileFile> builder)
    {
        builder.Property(p => p.ProfileId).IsRequired();
        builder.HasOne<Profile>(p => p.Profile).WithMany().HasForeignKey(p => p.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(p => p.File).IsRequired();
    }
}