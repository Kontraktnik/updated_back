using Application.DTO.Common;
using Application.DTO.User;
using MediatR;

namespace Application.Features.SurveyCQRS.Command.DeleteSurvey;

public class DeleteSurveyCommand : IRequest<ResponseDTO<bool>>
{
    public DeleteSurveyCommand(long id, UserDTO userDto)
    {
        Id = id;
        UserDto = userDto;
    }

    public long Id { get; set; }
    public UserDTO UserDto { get; set; }


}