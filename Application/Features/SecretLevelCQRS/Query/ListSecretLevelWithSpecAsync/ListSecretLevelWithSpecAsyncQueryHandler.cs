using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.ListSecretLevelWithSpecAsync;

public class ListSecretLevelWithSpecAsyncQueryHandler : IRequestHandler<ListSecretLevelWithSpecAsyncQuery,ResponseDTO<ICollection<SecretLevelDTO>>>
{
    private readonly ISecretLevelRepository _repository;
    private readonly IMapper _mapper;

    public ListSecretLevelWithSpecAsyncQueryHandler(ISecretLevelRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<SecretLevelDTO>>> Handle(ListSecretLevelWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<SecretLevelDTO>>(models);
        return new ResponseDTO<ICollection<SecretLevelDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}