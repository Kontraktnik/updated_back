using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VTShCQRS.Query.GetVTShWithSpecAsync;

public class GetVTShWithSpecAsyncQueryHandler : IRequestHandler<GetVTShWithSpecAsyncQuery,ResponseDTO<VTShDTO>>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetVTShWithSpecAsyncQueryHandler(IVTShRepository vtShRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<VTShDTO>> Handle(GetVTShWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var vtSh = await _vtShRepository.GetEntityWithSpecAsync(request.specification);
        if (vtSh != null)
        {
            return new ResponseDTO<VTShDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<VTShDTO>(vtSh),
            };
        }
        else
        {
            return new ResponseDTO<VTShDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}