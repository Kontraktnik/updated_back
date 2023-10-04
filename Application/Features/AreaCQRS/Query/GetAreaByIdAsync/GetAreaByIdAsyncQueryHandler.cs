using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.AreaCQRS.Query.GetAreaByIdAsync;

public class GetAreaByIdAsyncQueryHandler : IRequestHandler<GetAreaByIdAsyncQuery,ResponseDTO<AreaDTO>>
{
    private readonly IAreaRepository _areaRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> _localizer;

    public GetAreaByIdAsyncQueryHandler(IAreaRepository areaRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
        _localizer = localizer;
    }
    
    public async Task<ResponseDTO<AreaDTO>> Handle(GetAreaByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var area = await _areaRepository.GetByIdAsync(request.Id);
            if (area != null)
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