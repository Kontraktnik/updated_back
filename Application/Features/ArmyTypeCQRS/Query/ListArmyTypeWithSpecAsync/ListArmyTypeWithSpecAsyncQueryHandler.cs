using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;

public class ListArmyTypeWithSpecAsyncQueryHandler : IRequestHandler<ListArmyTypeWithSpecAsyncQuery,ResponseDTO<ICollection<ArmyTypeDTO>>>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;

    public ListArmyTypeWithSpecAsyncQueryHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<ArmyTypeDTO>>> Handle(ListArmyTypeWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyTypes = await _armyTypeRepository.ListWithSpecAsync(request.specification);
        var armyTypesDTO = _mapper.Map<ICollection<ArmyTypeDTO>>(armyTypes);
        return new ResponseDTO<ICollection<ArmyTypeDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = armyTypesDTO,
        };
    }
}