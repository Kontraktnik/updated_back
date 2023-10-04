using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.DigitalSign;
using Domain.Models.DigitalSignModels;
using MediatR;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSignInfoById;

public class GetDigitalSignInfoById : IRequest<ResponseDTO<DigitalSignInfoDTO>>
{
    public ISpecification<DigitalSignInfo> specification { get; set; }

    public GetDigitalSignInfoById(ISpecification<DigitalSignInfo> _specification)
    {
        this.specification = _specification;
    }
}
