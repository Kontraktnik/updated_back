using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Command.DeleteArmyRegion;

public class DeleteArmyRegionCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteArmyRegionCommand(long Id)
    {
        this.Id = Id;
    }
}