using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VTShCQRS.Query.GetVTShByIdAsync;

public class GetVTShByIdAsyncQueryHandler : IRequestHandler<GetVTShByIdAsyncQuery,ResponseDTO<VTShDTO>>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetVTShByIdAsyncQueryHandler(IVTShRepository vtShRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<VTShDTO>> Handle(GetVTShByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var vtSh = await _vtShRepository.GetByIdAsync(request.Id);
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
                Message = this.localizer["NotFound"]
            };
        }
    }
}