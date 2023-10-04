using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.DigitalSign;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSignInfoById;

public class GetDigitalSignInfoByIdHandler : IRequestHandler<GetDigitalSignInfoById, ResponseDTO<DigitalSignInfoDTO>>
{
    private readonly IDigitalSignInfoRepository _digitalSignInfoRepository;
    private readonly IMapper _mapper;

    public GetDigitalSignInfoByIdHandler(IMapper mapper,
        IDigitalSignInfoRepository digitalSignInfoRepository)
    {
        _digitalSignInfoRepository = digitalSignInfoRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<DigitalSignInfoDTO>> Handle(GetDigitalSignInfoById request, CancellationToken cancellationToken)
    {
        try
        {
            var digitalSignInfo = await _digitalSignInfoRepository.ListWithSpecAsync(request.specification);

            return new ResponseDTO<DigitalSignInfoDTO>
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<DigitalSignInfoDTO>(digitalSignInfo)
            };
        }
        catch(Exception ex)
        {
            return new ResponseDTO<DigitalSignInfoDTO>
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}
