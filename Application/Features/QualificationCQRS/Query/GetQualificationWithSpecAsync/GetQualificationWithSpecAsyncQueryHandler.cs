using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionWithSpecAsync;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.QualificationCQRS.Query.GetQualificationWithSpecAsync;

public class GetQualificationWithSpecAsyncQueryHandler : IRequestHandler<GetQualificationWithSpecAsyncQuery,ResponseDTO<QualificationDTO>>
{
    private readonly IQualificationRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetQualificationWithSpecAsyncQueryHandler(IQualificationRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<QualificationDTO>> Handle(GetQualificationWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
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