using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RelativeCQRS.Query.ListRelativeAllAsync;

public class ListRelativeAllAsyncQueryHandler : IRequestHandler<ListRelativeAllAsyncQuery,ResponseDTO<ICollection<RelativeDTO>>>
{
    private readonly IRelativeRepository _repository;
    private readonly IMapper _mapper;

    public ListRelativeAllAsyncQueryHandler(IRelativeRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<RelativeDTO>>> Handle(ListRelativeAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<RelativeDTO>>(models);
        return new ResponseDTO<ICollection<RelativeDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}