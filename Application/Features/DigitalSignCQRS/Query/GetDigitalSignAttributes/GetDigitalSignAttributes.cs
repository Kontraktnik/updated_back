using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.DigitalSign;
using Domain.Models.DigitalSignModels;
using MediatR;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSignAttributes;

public class GetDigitalSignAttributes : IRequest<ResponseDTO<ICollection<DigitalSignAttributeDTO>>>
{
    public ISpecification<DigitalSignAttribute> specification { get; set; }

    public GetDigitalSignAttributes(ISpecification<DigitalSignAttribute> _specification)
    {
        this.specification = _specification;
    }
}
