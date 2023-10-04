using Domain.Models.DigitalSignModels;

namespace Infrastracture.Contracts.Specifications.DigitalSignSpecification;

public class DigitalSignBinarySpecification : BaseSpecification<DigitalSignBinary>
{
    public DigitalSignBinarySpecification() { }

    public DigitalSignBinarySpecification(long Id) : base(p => p.Id.Equals(Id)) { }
}