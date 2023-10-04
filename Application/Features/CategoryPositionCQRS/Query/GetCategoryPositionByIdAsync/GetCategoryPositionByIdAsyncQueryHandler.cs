using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionByIdAsync;

public class GetCategoryPositionByIdAsyncQueryHandler : IRequestHandler<GetCategoryPositionByIdAsyncQuery,ResponseDTO<CategoryPositionDTO>>
{
    private readonly ICategoryPositionRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetCategoryPositionByIdAsyncQueryHandler(ICategoryPositionRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;

    }

    public async Task<ResponseDTO<CategoryPositionDTO>> Handle(GetCategoryPositionByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
        if (model != null)
        {
            return new ResponseDTO<CategoryPositionDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<CategoryPositionDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<CategoryPositionDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}