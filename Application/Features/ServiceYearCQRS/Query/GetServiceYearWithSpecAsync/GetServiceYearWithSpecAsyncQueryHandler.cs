using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionWithSpecAsync;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ServiceYearCQRS.Query.GetServiceYearWithSpecAsync;

public class GetServiceYearWithSpecAsyncQueryHandler : IRequestHandler<GetServiceYearWithSpecAsyncQuery,ResponseDTO<ServiceYearDTO>>
{
    private readonly IServiceYearRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetServiceYearWithSpecAsyncQueryHandler(IServiceYearRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ServiceYearDTO>> Handle(GetServiceYearWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<ServiceYearDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<ServiceYearDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<ServiceYearDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = this.localizer["NotFound"]
            };
        }
    }
}