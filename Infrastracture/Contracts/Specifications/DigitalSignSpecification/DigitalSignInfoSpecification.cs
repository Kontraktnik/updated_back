using Domain.Models.DigitalSignModels;

namespace Infrastracture.Contracts.Specifications.DigitalSignSpecification;

public class DigitalSignInfoSpecification : BaseSpecification<DigitalSignInfo>
{
    public DigitalSignInfoSpecification() { }

    public DigitalSignInfoSpecification(long Id) : base(p => p.Id.Equals(Id)) { }
}