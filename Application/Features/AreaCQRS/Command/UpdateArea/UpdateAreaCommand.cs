using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.AreaCQRS.Command.UpdateArea;

public class UpdateAreaCommand : IRequest<ResponseDTO<AreaDTO>>
{
    public long Id { get; set; }
    public AreaDTO model { get; set; }

    public UpdateAreaCommand(AreaDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}