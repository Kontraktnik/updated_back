using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.AreaCQRS.Command.AddArea;

public class AddAreaCommand : IRequest<ResponseDTO<AreaDTO>>
{
    public AreaDTO model { get; set; }

    public AddAreaCommand(AreaDTO model)
    {
        this.model = model;
    }
    
}