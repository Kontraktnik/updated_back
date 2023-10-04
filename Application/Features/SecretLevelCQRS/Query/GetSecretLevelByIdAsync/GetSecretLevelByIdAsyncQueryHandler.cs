using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SecretLevelCQRS.Query.GetSecretLevelByIdAsync;

public class GetSecretLevelByIdAsyncQueryHandler : IRequestHandler<GetSecretLevelByIdAsyncQuery,ResponseDTO<SecretLevelDTO>>
{
    private readonly ISecretLevelRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetSecretLevelByIdAsyncQueryHandler(ISecretLevelRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<SecretLevelDTO>> Handle(GetSecretLevelByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
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