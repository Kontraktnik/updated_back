using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.RelativeCQRS.Query.ListRelativeWithSpecAsync;

public class ListRelativeWithSpecAsyncQueryHandler : IRequestHandler<ListRelativeWithSpecAsyncQuery,ResponseDTO<ICollection<RelativeDTO>>>
{
    private readonly IRelativeRepository _repository;
    private readonly IMapper _mapper;

    public ListRelativeWithSpecAsyncQueryHandler(IRelativeRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<RelativeDTO>>> Handle(ListRelativeWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<RelativeDTO>>(models);
        return new ResponseDTO<ICollection<RelativeDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}