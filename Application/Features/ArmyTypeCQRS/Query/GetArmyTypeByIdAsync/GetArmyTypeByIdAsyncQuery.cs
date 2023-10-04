using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.GetArmyTypeByIdAsync;

public class GetArmyTypeByIdAsyncQuery : IRequest<ResponseDTO<ArmyTypeDTO>>
{
    public  long Id { get; set; }

    public GetArmyTypeByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}