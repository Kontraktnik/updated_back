using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Query.ListMedicalStatusWithSpecAsync;

public class ListMedicalStatusWithSpecAsyncQueryHandler : IRequestHandler<ListMedicalStatusWithSpecAsyncQuery,ResponseDTO<ICollection<MedicalStatusDTO>>>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;

    public ListMedicalStatusWithSpecAsyncQueryHandler(IMedicalStatusRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<MedicalStatusDTO>>> Handle(ListMedicalStatusWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<MedicalStatusDTO>>(models);
        return new ResponseDTO<ICollection<MedicalStatusDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}