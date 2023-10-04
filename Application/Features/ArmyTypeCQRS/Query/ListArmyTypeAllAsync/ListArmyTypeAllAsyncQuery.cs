using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;

public class ListArmyTypeAllAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyTypeDTO>>>
{
    
}