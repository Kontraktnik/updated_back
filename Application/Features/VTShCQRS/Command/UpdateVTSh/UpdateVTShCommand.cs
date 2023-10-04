using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.VTShCQRS.Command.UpdateVTSh;

public class UpdateVTShCommand : IRequest<ResponseDTO<VTShDTO>>
{
    public long Id { get; set; }
    public VTShDTO model { get; set; }

    public UpdateVTShCommand(VTShDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}