using Application.DTO.Common;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Command.DeleteMedicalStatus;

public class DeleteMedicalStatusCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteMedicalStatusCommand(long Id)
    {
        this.Id = Id;
    }
}