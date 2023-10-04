using System.Net;
using Application.Contracts.Persistence;
using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.AreaCQRS.Query.GetAreaWithSpecAsync;

public class GetAreaWithSpecAsyncQueryHandler : IRequestHandler<GetAreaWithSpecAsyncQuery,ResponseDTO<AreaDTO>>
{
    private IAreaRepository _areaRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> _localizer;

    public GetAreaWithSpecAsyncQueryHandler(IAreaRepository areaRepository,IMapper mapper,   IStringLocalizer<Localize> localizer
        )
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<ResponseDTO<AreaDTO>> Handle(GetAreaWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {

            var area = await _areaRepository.GetEntityWithSpecAsync(request.specification);
            if (!area.Equals(null))
            {
                return new ResponseDTO<AreaDTO>()
                {
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = _mapper.Map<AreaDTO>(area)
                };
            }
            else
            {
                return new ResponseDTO<AreaDTO>()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = this._localizer["NotFound"]
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<AreaDTO>()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = this._localizer["UnexpectedError"] +":" + ex.ToString()
            };
        }

    }
}