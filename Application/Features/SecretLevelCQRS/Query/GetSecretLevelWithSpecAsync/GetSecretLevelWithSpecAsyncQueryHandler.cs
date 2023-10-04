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

namespace Application.Features.SecretLevelCQRS.Query.GetSecretLevelWithSpecAsync;

public class GetSecretLevelWithSpecAsyncQueryHandler : IRequestHandler<GetSecretLevelWithSpecAsyncQuery,ResponseDTO<SecretLevelDTO>>
{
    private readonly ISecretLevelRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetSecretLevelWithSpecAsyncQueryHandler(ISecretLevelRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<SecretLevelDTO>> Handle(GetSecretLevelWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<SecretLevelDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<SecretLevelDTO>(model),
            };
        }
        else
        {
            return new ResponseDTO<SecretLevelDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}