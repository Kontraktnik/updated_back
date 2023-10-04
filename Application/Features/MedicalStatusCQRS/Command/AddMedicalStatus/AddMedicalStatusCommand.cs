using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Command.AddMedicalStatus;

public class AddMedicalStatusCommand : IRequest<ResponseDTO<MedicalStatusDTO>>
{
    public MedicalStatusDTO model { get; set; }

    public AddMedicalStatusCommand(MedicalStatusDTO model)
    {
        this.model = model;
    }
}