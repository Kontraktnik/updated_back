using Application.Contracts.Persistence;
using Application.DTO.Common;
using AutoMapper;
using MediatR;

namespace Application.Features.PhoneNotificationCQRS.Command;

public class sendUserConfirmationCodeAgainCommandHandler : IRequestHandler<sendUserConfirmationCodeAgainCommand,ResponseDTO<bool>>
{
    private IPhoneNotificationRepository _phoneNotification;
    private IMapper _mapper;

    public sendUserConfirmationCodeAgainCommandHandler(IPhoneNotificationRepository phoneNotification,IMapper mapper)
    {
        _phoneNotification = phoneNotification;
        _mapper = mapper;
    }
    public async Task<ResponseDTO<bool>> Handle(sendUserConfirmationCodeAgainCommand request, CancellationToken cancellationToken)
    {
        return await _phoneNotification.sendUserConfirmationCodeAgain(request.IIN);
    }
}