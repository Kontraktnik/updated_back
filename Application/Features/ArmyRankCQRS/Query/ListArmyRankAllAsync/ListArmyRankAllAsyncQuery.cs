using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.ListArmyRankAllAsync;

public class ListArmyRankAllAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyRankDTO>>>
{
    
}