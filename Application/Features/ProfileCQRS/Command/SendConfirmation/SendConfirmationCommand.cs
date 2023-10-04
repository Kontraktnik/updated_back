using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.User;
using MediatR;

namespace Application.Features.ProfileCQRS.Command.SendConfirmation;

public class SendConfirmationCommand : IRequest<ResponseDTO<ProfileDTO>>
{
    public long ProfileId;
    public UserDTO CurrentUser;
    public SendConfirmationDTO model;

    public SendConfirmationCommand(long ProfileId,UserDTO CurrentUser,SendConfirmationDTO model)
    {
        this.ProfileId = ProfileId;
        this.CurrentUser = CurrentUser;
        this.model = model;
    }



}