using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.User;
using MediatR;

namespace Application.Features.ProfileFileCQRS.Command;

public class AddProfileFileCommand : IRequest<ResponseDTO<ProfileFileDTO>>
{
    public AddProfileFileCommand(string fileName, string extensionFile, ProfileFileCUDTO model, UserDTO userDto)
    {
        FileName = fileName;
        ExtensionFile = extensionFile;
        this.model = model;
        UserDto = userDto;
    }

    public string FileName { get; set; }
    public string ExtensionFile { get; set; }
    public ProfileFileCUDTO model { get; set; }
    public UserDTO UserDto { get; set; }
    
    



}