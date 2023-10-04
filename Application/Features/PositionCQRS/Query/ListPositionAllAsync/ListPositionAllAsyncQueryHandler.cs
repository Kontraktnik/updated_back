using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.PositionCQRS.Query.ListPositionAllAsync;

public class ListPositionAllAsyncQueryHandler : IRequestHandler<ListPositionAllAsyncQuery,ResponseDTO<ICollection<PositionRDTO>>>
{
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;

    public ListPositionAllAsyncQueryHandler(IPositionRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<PositionRDTO>>> Handle(ListPositionAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<PositionRDTO>>(models);
        return new ResponseDTO<ICollection<PositionRDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}