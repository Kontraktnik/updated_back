using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.DigitalSign;
using Domain.Models.DigitalSignModels;
using MediatR;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSigns;

public class GetDigitalSigns : IRequest<ResponseDTO<ICollection<DigitalSignDTO>>>
{
    public ISpecification<DigitalSign> specification { get; set; }

    public GetDigitalSigns(ISpecification<DigitalSign> _specification)
    {
        this.specification = _specification;
    }
}
