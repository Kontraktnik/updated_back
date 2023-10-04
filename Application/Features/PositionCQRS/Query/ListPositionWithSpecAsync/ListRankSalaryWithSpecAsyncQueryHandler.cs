using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.PositionCQRS.Query.ListPositionWithSpecAsync;

public class ListPositionWithSpecAsyncQueryHandler : IRequestHandler<ListPositionWithSpecAsyncQuery,ResponseDTO<ICollection<PositionRDTO>>>
{
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;

    public ListPositionWithSpecAsyncQueryHandler(IPositionRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<PositionRDTO>>> Handle(ListPositionWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<PositionRDTO>>(models);
        return new ResponseDTO<ICollection<PositionRDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}