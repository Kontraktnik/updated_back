using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionWithSpecAsync;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.RelativeCQRS.Query.GetRelativeWithSpecAsync;

public class GetRelativeWithSpecAsyncQueryHandler : IRequestHandler<GetRelativeWithSpecAsyncQuery,ResponseDTO<RelativeDTO>>
{
    private readonly IRelativeRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetRelativeWithSpecAsyncQueryHandler(IRelativeRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<RelativeDTO>> Handle(GetRelativeWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<RelativeDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<RelativeDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<RelativeDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}