using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.EducationCQRS.Query.GetEducationByIdAsync;

public class GetEducationByIdAsyncQueryHandler : IRequestHandler<GetEducationByIdAsyncQuery,ResponseDTO<EducationDTO>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetEducationByIdAsyncQueryHandler(IEducationRepository educationRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<EducationDTO>> Handle(GetEducationByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var education = await _educationRepository.GetByIdAsync(request.Id);
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
                Message = localizer["NotFound"]
            };
        }
    }
}