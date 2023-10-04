using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Query.GetArmyRegionByIdAsync;

public class GetArmyRegionByIdAsyncQuery : IRequest<ResponseDTO<ArmyRegionDTO>>
{
    public long Id { get; set; }

    public GetArmyRegionByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}