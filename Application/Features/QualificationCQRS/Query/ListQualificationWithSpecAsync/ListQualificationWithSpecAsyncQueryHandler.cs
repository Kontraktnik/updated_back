using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.ListQualificationWithSpecAsync;

public class ListQualificationWithSpecAsyncQueryHandler : IRequestHandler<ListQualificationWithSpecAsyncQuery,ResponseDTO<ICollection<QualificationDTO>>>
{
    private readonly IQualificationRepository _repository;
    private readonly IMapper _mapper;

    public ListQualificationWithSpecAsyncQueryHandler(IQualificationRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<QualificationDTO>>> Handle(ListQualificationWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<QualificationDTO>>(models);
        return new ResponseDTO<ICollection<QualificationDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}