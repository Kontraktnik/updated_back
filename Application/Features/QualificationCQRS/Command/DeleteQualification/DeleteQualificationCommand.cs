using Application.DTO.Common;
using MediatR;

namespace Application.Features.QualificationCQRS.Command.DeleteQualification;

public class DeleteQualificationCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteQualificationCommand(long Id)
    {
        this.Id = Id;
    }
}