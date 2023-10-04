using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.DigitalSign;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSignAttributes;

public class GetDigitalSignAttributesHandler : IRequestHandler<GetDigitalSignAttributes, ResponseDTO<ICollection<DigitalSignAttributeDTO>>>
{
    private readonly IDigitalSignAttributeRepository _digitalSignAttributeRepository;
    private readonly IMapper _mapper;

    public GetDigitalSignAttributesHandler(IMapper mapper,
        IDigitalSignAttributeRepository digitalSignAttributeRepository)
    {
        _digitalSignAttributeRepository = digitalSignAttributeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<DigitalSignAttributeDTO>>> Handle(GetDigitalSignAttributes request, CancellationToken cancellationToken)
    {
        try
        {
            var digitalSignAttributes = await _digitalSignAttributeRepository.ListWithSpecAsync(request.specification);

            return new ResponseDTO<ICollection<DigitalSignAttributeDTO>>
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<ICollection<DigitalSignAttributeDTO>>(digitalSignAttributes)
            };
        }
        catch(Exception ex)
        {
            return new ResponseDTO<ICollection<DigitalSignAttributeDTO>>
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}
