using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.EducationCQRS.Command.AddEducation;

public class AddEducationCommand : IRequest<ResponseDTO<EducationDTO>>
{
    public EducationDTO model { get; set; }

    public AddEducationCommand(EducationDTO model)
    {
        this.model = model;
    }
}