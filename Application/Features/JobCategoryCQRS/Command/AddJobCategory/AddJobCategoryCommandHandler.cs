using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobCategoryCQRS.Command.AddJobCategory;

public class AddJobCategoryCommandHandler : IRequestHandler<AddJobCategoryCommand,ResponseDTO<JobCategoryDTO>>

{
    private readonly IJobCategoryRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddJobCategoryCommandHandler(IJobCategoryRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<JobCategoryDTO>> Handle(AddJobCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<JobCategory>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<JobCategoryDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<JobCategoryDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<JobCategoryDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}