using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.EducationCQRS.Query.GetEducationWithSpecAsync;

public class GetEducationWithSpecAsyncQueryHandler : IRequestHandler<GetEducationWithSpecAsyncQuery,ResponseDTO<EducationDTO>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetEducationWithSpecAsyncQueryHandler(IEducationRepository educationRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<EducationDTO>> Handle(GetEducationWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var education = await _educationRepository.GetEntityWithSpecAsync(request.specification);
        if (education != null)
        {
            return new ResponseDTO<EducationDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<EducationDTO>(education),
            };
        }
        else
        {
            return new ResponseDTO<EducationDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = this.localizer["NotFound"]
            };
        }
    }
}