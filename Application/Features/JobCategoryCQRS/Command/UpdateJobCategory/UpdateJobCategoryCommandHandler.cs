using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobCategoryCQRS.Command.UpdateJobCategory;

public class UpdateJobCategoryCommandHandler : IRequestHandler<UpdateJobCategoryCommand,ResponseDTO<JobCategoryDTO>>
{
    
    private readonly IJobCategoryRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateJobCategoryCommandHandler(IJobCategoryRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<JobCategoryDTO>> Handle(UpdateJobCategoryCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<JobCategory>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<JobCategoryDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<JobCategoryDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<JobCategoryDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<JobCategoryDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.Message,
            };
        }
    }
}