using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.DigitalSign;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSigns;

public class GetDigitalSignsHandler : IRequestHandler<GetDigitalSigns, ResponseDTO<ICollection<DigitalSignDTO>>>
{
    private readonly IDigitalSignRepository _digitalSignRepository;
    private readonly IMapper _mapper;

    public GetDigitalSignsHandler(IMapper mapper,
        IDigitalSignRepository digitalSignRepository)
    {
        _digitalSignRepository = digitalSignRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<DigitalSignDTO>>> Handle(GetDigitalSigns request, CancellationToken cancellationToken)
    {
        try
        {
            var digitalSignInfo = await _digitalSignRepository.ListWithSpecAsync(request.specification);

            return new ResponseDTO<ICollection<DigitalSignDTO>>
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<ICollection<DigitalSignDTO>>(digitalSignInfo)
            };
        }
        catch(Exception ex)
        {
            return new ResponseDTO<ICollection<DigitalSignDTO>>
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}
