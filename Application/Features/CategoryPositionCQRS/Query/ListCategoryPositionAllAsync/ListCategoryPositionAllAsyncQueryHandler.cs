using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.ListCategoryPositionAllAsync;

public class ListCategoryPositionAllAsyncQueryHandler : IRequestHandler<ListCategoryPositionAllAsyncQuery,ResponseDTO<ICollection<CategoryPositionDTO>>>
{
    private readonly ICategoryPositionRepository _repository;
    private readonly IMapper _mapper;

    public ListCategoryPositionAllAsyncQueryHandler(ICategoryPositionRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<CategoryPositionDTO>>> Handle(ListCategoryPositionAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<CategoryPositionDTO>>(models);
        return new ResponseDTO<ICollection<CategoryPositionDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}