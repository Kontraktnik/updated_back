using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.EducationCQRS.Command.UpdateEducation;

public class UpdateEducationCommand : IRequest<ResponseDTO<EducationDTO>>
{
    public long Id { get; set; }
    public EducationDTO model { get; set; }

    public UpdateEducationCommand(EducationDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}