using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Query.ListJobCategoryAllAsync;

public class ListJobCategoryAllAsyncQueryHandler : IRequestHandler<ListJobCategoryAllAsyncQuery,ResponseDTO<ICollection<JobCategoryDTO>>>
{
    private readonly IJobCategoryRepository _repository;
    private readonly IMapper _mapper;

    public ListJobCategoryAllAsyncQueryHandler(IJobCategoryRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<JobCategoryDTO>>> Handle(ListJobCategoryAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<JobCategoryDTO>>(models);
        return new ResponseDTO<ICollection<JobCategoryDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}