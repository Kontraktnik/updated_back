using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.CategoryPositionCQRS.Command.AddCategoryPosition;

public class AddCategoryPositionCommandHandler : IRequestHandler<AddCategoryPositionCommand,ResponseDTO<CategoryPositionDTO>>
{
    private readonly ICategoryPositionRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddCategoryPositionCommandHandler(ICategoryPositionRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<CategoryPositionDTO>> Handle(AddCategoryPositionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<CategoryPosition>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<CategoryPositionDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<CategoryPositionDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<CategoryPositionDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}