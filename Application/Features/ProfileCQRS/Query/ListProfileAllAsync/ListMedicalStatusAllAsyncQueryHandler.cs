using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.ListProfileAllAsync;

public class ListProfileAllAsyncQueryHandler : IRequestHandler<ListProfileAllAsyncQuery,ResponseDTO<ICollection<ProfileDTO>>>
{
    private readonly IProfileRepository _repository;
    private readonly IMapper _mapper;

    public ListProfileAllAsyncQueryHandler(IProfileRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<ProfileDTO>>> Handle(ListProfileAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<ProfileDTO>>(models);
        return new ResponseDTO<ICollection<ProfileDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}