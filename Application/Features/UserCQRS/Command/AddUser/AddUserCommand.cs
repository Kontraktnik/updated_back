using Application.DTO.Common;
using Application.DTO.User;
using MediatR;

namespace Application.Features.UserCQRS.Command.AddUser;

public class AddUserCommand : IRequest<ResponseDTO<UserDTO>>
{
    public UserCreateDTO model { get; set; }

    public AddUserCommand(UserCreateDTO _model)
    {
        model = _model;
    }
}