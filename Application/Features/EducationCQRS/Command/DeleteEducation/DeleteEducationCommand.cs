using Application.DTO.Common;
using MediatR;

namespace Application.Features.EducationCQRS.Command.DeleteEducation;

public class DeleteEducationCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteEducationCommand(long Id)
    {
        this.Id = Id;
    }
}