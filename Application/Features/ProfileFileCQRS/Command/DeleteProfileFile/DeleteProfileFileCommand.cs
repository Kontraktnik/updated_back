using Application.DTO.Common;
using Application.DTO.User;
using MediatR;

namespace Application.Features.ProfileFileCQRS.Command.DeleteProfileFile;

public class DeleteProfileFileCommand : IRequest<ResponseDTO<bool>>
{
    public DeleteProfileFileCommand(long id, UserDTO currentUser)
    {
        Id = id;
        this.currentUser = currentUser;
    }

    public long Id { get; set; }
    public UserDTO currentUser { get; set; }
    
    
    
}