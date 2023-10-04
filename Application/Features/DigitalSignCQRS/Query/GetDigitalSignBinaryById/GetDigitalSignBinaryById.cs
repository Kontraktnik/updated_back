using Application.Contracts.Specification;
using Application.DTO.Common;
using Domain.Models.DigitalSignModels;
using MediatR;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSignBinaryById;

public class GetDigitalSignBinaryById : IRequest<ResponseDTO<byte[]>>
{
    public ISpecification<DigitalSignBinary> specification { get; set; }

    public GetDigitalSignBinaryById(ISpecification<DigitalSignBinary> _specification)
    {
        this.specification = _specification;
    }
}
