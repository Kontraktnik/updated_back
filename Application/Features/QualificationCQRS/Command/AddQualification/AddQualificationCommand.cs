using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.QualificationCQRS.Command.AddQualification;

public class AddQualificationCommand : IRequest<ResponseDTO<QualificationDTO>>
{
    public QualificationDTO model { get; set; }

    public AddQualificationCommand(QualificationDTO model)
    {
        this.model = model;
    }
}