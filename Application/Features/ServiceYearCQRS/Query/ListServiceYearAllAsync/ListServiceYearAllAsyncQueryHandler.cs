using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.ListServiceYearAllAsync;

public class ListServiceYearAllAsyncQueryHandler : IRequestHandler<ListServiceYearAllAsyncQuery,ResponseDTO<ICollection<ServiceYearDTO>>>
{
    private readonly IServiceYearRepository _repository;
    private readonly IMapper _mapper;

    public ListServiceYearAllAsyncQueryHandler(IServiceYearRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<ServiceYearDTO>>> Handle(ListServiceYearAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<ServiceYearDTO>>(models);
        return new ResponseDTO<ICollection<ServiceYearDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}