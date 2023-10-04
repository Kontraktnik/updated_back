using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Query.ListJobCategoryWithSpecAsync;

public class ListJobCategoryWithSpecAsyncQueryHandler : IRequestHandler<ListJobCategoryWithSpecAsyncQuery,ResponseDTO<ICollection<JobCategoryDTO>>>
{
    private readonly IJobCategoryRepository _repository;
    private readonly IMapper _mapper;

    public ListJobCategoryWithSpecAsyncQueryHandler(IJobCategoryRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<JobCategoryDTO>>> Handle(ListJobCategoryWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<JobCategoryDTO>>(models);
        return new ResponseDTO<ICollection<JobCategoryDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}