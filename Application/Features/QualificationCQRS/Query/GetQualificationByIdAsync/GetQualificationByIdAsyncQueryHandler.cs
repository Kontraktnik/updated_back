using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.QualificationCQRS.Query.GetQualificationByIdAsync;

public class GetQualificationByIdAsyncQueryHandler : IRequestHandler<GetQualificationByIdAsyncQuery,ResponseDTO<QualificationDTO>>
{
    private readonly IQualificationRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetQualificationByIdAsyncQueryHandler(IQualificationRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<QualificationDTO>> Handle(GetQualificationByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
        if (model != null)
        {
            return new ResponseDTO<QualificationDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<QualificationDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<QualificationDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}