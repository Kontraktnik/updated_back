using Application.Contracts.Persistence;
using Application.DTO.Common;
using MediatR;
using System.Net;

namespace Application.Features.DigitalSignCQRS.Query.GetDigitalSignBinaryById;

public class GetDigitalSignBinaryByIdHandler : IRequestHandler<GetDigitalSignBinaryById, ResponseDTO<byte[]>>
{
    private readonly IDigitalSignBinaryRepository _digitalSignBinaryRepository;

    public GetDigitalSignBinaryByIdHandler(IDigitalSignBinaryRepository digitalSignBinaryRepository)
    {
        _digitalSignBinaryRepository = digitalSignBinaryRepository;
    }

    public async Task<ResponseDTO<byte[]>> Handle(GetDigitalSignBinaryById request, CancellationToken cancellationToken)
    {
        try
        {
            var digitalSignBinary = await _digitalSignBinaryRepository.GetEntityWithSpecAsync(request.specification);

            return new ResponseDTO<byte[]>
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Data = digitalSignBinary.Data
            };
        }
        catch(Exception ex)
        {
            return new ResponseDTO<byte[]>
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}
