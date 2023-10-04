using Application.DTO.Common;
using Application.DTO.User;
using MediatR;

namespace Application.Features.VacancyCQRS.Command.DeleteVacancy;

public class DeleteVacancyCommand : IRequest<ResponseDTO<bool>>
{
    public long Id { get; set; }
    public UserDTO UserDto { get; set; }
    public DeleteVacancyCommand(long Id,UserDTO userDto)
    {
        this.Id = Id;
        UserDto = userDto;
    }
}