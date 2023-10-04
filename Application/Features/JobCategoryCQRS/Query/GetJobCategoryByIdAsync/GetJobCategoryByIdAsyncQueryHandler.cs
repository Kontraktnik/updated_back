using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobCategoryCQRS.Query.GetJobCategoryByIdAsync;

public class GetJobCategoryByIdAsyncQueryHandler : IRequestHandler<GetJobCategoryByIdAsyncQuery,ResponseDTO<JobCategoryDTO>>
{
    private readonly IJobCategoryRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetJobCategoryByIdAsyncQueryHandler(IJobCategoryRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<JobCategoryDTO>> Handle(GetJobCategoryByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
        if (model != null)
        {
            return new ResponseDTO<JobCategoryDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<JobCategoryDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<JobCategoryDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}