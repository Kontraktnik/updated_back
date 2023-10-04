using Application.DTO.Common;
using Application.DTO.User;
using MediatR;

namespace Application.Features.UserCQRS.Command.UpdateUser;

public class UpdateUserCommand : IRequest<ResponseDTO<UserDTO>>
{
    public long Id { get; set; }
    public UserUpdateDTO model { get; set; }

    public UpdateUserCommand(long _Id, UserUpdateDTO _model)
    {
        model = _model;
        model.Id = _Id;
        Id = _Id;
    }
    

}