using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Query.ListRankSalaryWithSpecAsync;

public class ListRankSalaryWithSpecAsyncQueryHandler : IRequestHandler<ListRankSalaryWithSpecAsyncQuery,ResponseDTO<ICollection<RankSalaryRDTO>>>
{
    private readonly IRankSalaryRepository _repository;
    private readonly IMapper _mapper;

    public ListRankSalaryWithSpecAsyncQueryHandler(IRankSalaryRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<RankSalaryRDTO>>> Handle(ListRankSalaryWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<RankSalaryRDTO>>(models);
        return new ResponseDTO<ICollection<RankSalaryRDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}