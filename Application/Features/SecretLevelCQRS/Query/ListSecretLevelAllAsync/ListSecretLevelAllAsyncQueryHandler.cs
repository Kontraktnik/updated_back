using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.ListSecretLevelAllAsync;

public class ListSecretLevelAllAsyncQueryHandler : IRequestHandler<ListSecretLevelAllAsyncQuery,ResponseDTO<ICollection<SecretLevelDTO>>>
{
    private readonly ISecretLevelRepository _repository;
    private readonly IMapper _mapper;

    public ListSecretLevelAllAsyncQueryHandler(ISecretLevelRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<SecretLevelDTO>>> Handle(ListSecretLevelAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<SecretLevelDTO>>(models);
        return new ResponseDTO<ICollection<SecretLevelDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}