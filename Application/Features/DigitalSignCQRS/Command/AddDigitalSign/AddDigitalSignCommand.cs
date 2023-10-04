using Application.DTO.Common;
using Application.DTO.DigitalSign;
using MediatR;

namespace Application.Features.DigitalSignCQRS.Command.AddDigitalSign;

public class AddDigitalSignCommand : IRequest<ResponseDTO<long?>>
{
    public DigitalSignBaseDTO model;
    public byte[] binaryData;
    public DigitalSignInfoDTO digitalSignInfo;

    public AddDigitalSignCommand(DigitalSignBaseDTO _model, byte[] _binaryData, DigitalSignInfoDTO _digitalSignInfo)
    {
        model = _model;
        binaryData = _binaryData;
        digitalSignInfo = _digitalSignInfo;
    }
}