using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.RankSalaryCQRS.Query.GetRankSalaryByIdAsync;

public class GetQualificationByIdAsyncQueryHandler : IRequestHandler<GetRankSalaryByIdAsyncQuery,ResponseDTO<RankSalaryRDTO>>
{
    private readonly IRankSalaryRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetQualificationByIdAsyncQueryHandler(IRankSalaryRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<RankSalaryRDTO>> Handle(GetRankSalaryByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<RankSalaryRDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<RankSalaryRDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<RankSalaryRDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}