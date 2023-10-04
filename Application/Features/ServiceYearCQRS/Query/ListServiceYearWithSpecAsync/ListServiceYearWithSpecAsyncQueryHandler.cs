using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.ListServiceYearWithSpecAsync;

public class ListServiceYearWithSpecAsyncQueryHandler : IRequestHandler<ListServiceYearWithSpecAsyncQuery,ResponseDTO<ICollection<ServiceYearDTO>>>
{
    private readonly IServiceYearRepository _repository;
    private readonly IMapper _mapper;

    public ListServiceYearWithSpecAsyncQueryHandler(IServiceYearRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<ServiceYearDTO>>> Handle(ListServiceYearWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<ServiceYearDTO>>(models);
        return new ResponseDTO<ICollection<ServiceYearDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}