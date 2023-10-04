using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionWithSpecAsync;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobYearCQRS.Query.GetJobYearWithSpecAsync;

public class GetJobYearWithSpecAsyncQueryHandler : IRequestHandler<GetJobYearWithSpecAsyncQuery,ResponseDTO<JobYearRDTO>>
{
    private readonly IJobYearRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetJobYearWithSpecAsyncQueryHandler(IJobYearRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<JobYearRDTO>> Handle(GetJobYearWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<JobYearRDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<JobYearRDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<JobYearRDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}