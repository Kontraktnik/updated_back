using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.System;
using Application.Features.MedicalStatusCQRS.Query.GetMedicalStatusByIdAsync;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileCQRS.Query.GetProfileByIdAsync;

public class GetProfileByIdAsyncQueryHandler : IRequestHandler<GetProfileByIdAsyncQuery,ResponseDTO<ProfileDTO>>
{
    private readonly IProfileRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetProfileByIdAsyncQueryHandler(IProfileRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ProfileDTO>> Handle(GetProfileByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
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