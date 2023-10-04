using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.VTShCQRS.Command.AddVTSh;

public class AddVTShCommand : IRequest<ResponseDTO<VTShDTO>>
{
    public VTShDTO model { get; set; }

    public AddVTShCommand(VTShDTO model)
    {
        this.model = model;
    }
}