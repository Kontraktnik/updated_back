using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;

public class ListArmyTypeAllAsyncQueryHandler : IRequestHandler<ListArmyTypeAllAsyncQuery,ResponseDTO<ICollection<ArmyTypeDTO>>>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;

    public ListArmyTypeAllAsyncQueryHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<ArmyTypeDTO>>> Handle(ListArmyTypeAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyTypes = await _armyTypeRepository.ListAllAsync();
        var armyTypesDTO = _mapper.Map<ICollection<ArmyTypeDTO>>(armyTypes);
        return new ResponseDTO<ICollection<ArmyTypeDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = armyTypesDTO,
        };
    }
}