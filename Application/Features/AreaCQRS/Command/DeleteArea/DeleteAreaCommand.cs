using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.AreaCQRS.Command.DeleteArea;

public class DeleteAreaCommand : IRequest<ResponseDTO<bool>>
{
    public long Id { get; set; }

    public DeleteAreaCommand(long Id)
    {
        this.Id = Id;
    }
}