using Domain.Models.DigitalSignModels;

namespace Infrastracture.Contracts.Specifications.DigitalSignSpecification;

public class DigitalSignAttributeSpecification : BaseSpecification<DigitalSignAttribute>
{
    public DigitalSignAttributeSpecification() { }

    public DigitalSignAttributeSpecification(long Id) : base(p => p.Id.Equals(Id)) { }
}