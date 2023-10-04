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

namespace Application.Features.MedicalStatusCQRS.Query.GetMedicalStatusWithSpecAsync;

public class GetMedicalStatusWithSpecAsyncQueryHandler : IRequestHandler<GetMedicalStatusWithSpecAsyncQuery,ResponseDTO<MedicalStatusDTO>>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetMedicalStatusWithSpecAsyncQueryHandler(IMedicalStatusRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<MedicalStatusDTO>> Handle(GetMedicalStatusWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<MedicalStatusDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<MedicalStatusDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<MedicalStatusDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = this.localizer["NotFound"]
            };
        }
    }
}