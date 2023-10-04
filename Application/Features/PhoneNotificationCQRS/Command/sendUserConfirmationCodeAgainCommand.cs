using Application.DTO.Auth;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.PhoneNotificationCQRS.Command;

public class sendUserConfirmationCodeAgainCommand : IRequest<ResponseDTO<bool>>
{
    public string IIN { get; set; }

    public sendUserConfirmationCodeAgainCommand(string IIN)
    {
        this.IIN = IIN;
    }
}