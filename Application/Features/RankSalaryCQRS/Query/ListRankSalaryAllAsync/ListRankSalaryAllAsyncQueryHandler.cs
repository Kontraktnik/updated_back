using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Query.ListRankSalaryAllAsync;

public class ListRankSalaryAllAsyncQueryHandler : IRequestHandler<ListRankSalaryAllAsyncQuery,ResponseDTO<ICollection<RankSalaryRDTO>>>
{
    private readonly IRankSalaryRepository _repository;
    private readonly IMapper _mapper;

    public ListRankSalaryAllAsyncQueryHandler(IRankSalaryRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<RankSalaryRDTO>>> Handle(ListRankSalaryAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<RankSalaryRDTO>>(models);
        return new ResponseDTO<ICollection<RankSalaryRDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}