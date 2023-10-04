using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.ListCategoryPositionWithSpecAsync;

public class ListArmyTypeWithSpecAsyncQueryHandler : IRequestHandler<ListCategoryPositionWithSpecAsyncQuery,ResponseDTO<ICollection<CategoryPositionDTO>>>
{
    private readonly ICategoryPositionRepository _repository;
    private readonly IMapper _mapper;

    public ListArmyTypeWithSpecAsyncQueryHandler(ICategoryPositionRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<CategoryPositionDTO>>> Handle(ListCategoryPositionWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<CategoryPositionDTO>>(models);
        return new ResponseDTO<ICollection<CategoryPositionDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}