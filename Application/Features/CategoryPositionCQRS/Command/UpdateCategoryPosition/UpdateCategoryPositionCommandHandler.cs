using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.CategoryPositionCQRS.Command.UpdateCategoryPosition;

public class UpdateCategoryPositionCommandHandler : IRequestHandler<UpdateCategoryPositionCommand,ResponseDTO<CategoryPositionDTO>>
{
    private readonly ICategoryPositionRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateCategoryPositionCommandHandler(ICategoryPositionRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<CategoryPositionDTO>> Handle(UpdateCategoryPositionCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<CategoryPosition>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<CategoryPositionDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<CategoryPositionDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<CategoryPositionDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<CategoryPositionDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}