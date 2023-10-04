using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Query.ListArmyRegionAllAsync;

public class ListArmyRegionAllAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyRegionDTO>>>
{
    
}