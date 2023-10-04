using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Command.UpdateMedicalStatus;

public class UpdateMedicalStatusCommand : IRequest<ResponseDTO<MedicalStatusDTO>>
{
    public long Id { get; set; }
    public MedicalStatusDTO model { get; set; }

    public UpdateMedicalStatusCommand(MedicalStatusDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}