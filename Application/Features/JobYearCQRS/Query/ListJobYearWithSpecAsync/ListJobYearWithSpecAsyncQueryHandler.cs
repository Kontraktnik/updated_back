using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.JobYearCQRS.Query.ListJobYearWithSpecAsync;

public class ListJobYearWithSpecAsyncQueryHandler : IRequestHandler<ListJobYearWithSpecAsyncQuery,ResponseDTO<ICollection<JobYearRDTO>>>
{
    private readonly IJobYearRepository _repository;
    private readonly IMapper _mapper;

    public ListJobYearWithSpecAsyncQueryHandler(IJobYearRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<JobYearRDTO>>> Handle(ListJobYearWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<JobYearRDTO>>(models);
        return new ResponseDTO<ICollection<JobYearRDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}