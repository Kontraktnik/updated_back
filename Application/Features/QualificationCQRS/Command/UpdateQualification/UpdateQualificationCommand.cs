using Application.DTO.Calculation;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.QualificationCQRS.Command.UpdateQualification;

public class UpdateQualificationCommand : IRequest<ResponseDTO<QualificationDTO>>
{
    public long Id { get; set; }
    public QualificationDTO model { get; set; }

    public UpdateQualificationCommand(QualificationDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}