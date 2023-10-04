using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.MedicalStatusCQRS.Query.GetMedicalStatusByIdAsync;

public class GetMedicalStatusByIdAsyncQueryHandler : IRequestHandler<GetMedicalStatusByIdAsyncQuery,ResponseDTO<MedicalStatusDTO>>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetMedicalStatusByIdAsyncQueryHandler(IMedicalStatusRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<MedicalStatusDTO>> Handle(GetMedicalStatusByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
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