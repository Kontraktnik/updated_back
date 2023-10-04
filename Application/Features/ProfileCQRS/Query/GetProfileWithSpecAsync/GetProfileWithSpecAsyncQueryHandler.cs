using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.System;
using Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionWithSpecAsync;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileCQRS.Query.GetProfileWithSpecAsync;

public class GetProfileWithSpecAsyncQueryHandler : IRequestHandler<GetProfileWithSpecAsyncQuery,ResponseDTO<ProfileDTO>>
{
    private readonly IProfileRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetProfileWithSpecAsyncQueryHandler(IProfileRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ProfileDTO>> Handle(GetProfileWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<ProfileDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<ProfileDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<ProfileDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}