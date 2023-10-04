using Domain.Models.DigitalSignModels;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelConfigurations.DigitalSignModelConf;

public class DigitalSignModelConfiguration : IEntityTypeConfiguration<DigitalSign>
{
    public void Configure(EntityTypeBuilder<DigitalSign> builder)
    {
        builder.Property(p => p.SurveyId).IsRequired();

        builder.HasOne<DigitalSignBinary>(p => p.BinaryData)
            .WithMany(p => p.DigitalSigns)
            .HasForeignKey(p => p.BinaryDataId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<DigitalSignInfo>(p => p.Info)
            .WithMany(p => p.DigitalSigns)
            .HasForeignKey(p => p.InfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Survey>(p => p.Survey)
            .WithMany(p => p.DigitalSigns)
            .HasForeignKey(p => p.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Step>(p => p.Step)
            .WithMany(p => p.DigitalSigns)
            .HasForeignKey(p => p.StepId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>(p => p.SignedUser)
            .WithMany(p => p.DigitalSigns)
            .HasForeignKey(p => p.WhoSignedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}