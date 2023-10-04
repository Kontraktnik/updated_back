using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.User;
using MediatR;

namespace Application.Features.ProfileCQRS.Command.SendRequest;

public class SendRequestCommand : IRequest<ResponseDTO<ProfileDTO>>
{
    public UserDTO user;
    public SendRequestDTO model;

    public SendRequestCommand(UserDTO user,SendRequestDTO model)
    {
        this.model = model;
        this.user = user;
    }


}