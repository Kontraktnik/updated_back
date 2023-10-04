using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Query.ListMedicalStatusAllAsync;

public class ListMedicalStatusAllAsyncQueryHandler : IRequestHandler<ListMedicalStatusAllAsyncQuery,ResponseDTO<ICollection<MedicalStatusDTO>>>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;

    public ListMedicalStatusAllAsyncQueryHandler(IMedicalStatusRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<MedicalStatusDTO>>> Handle(ListMedicalStatusAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<MedicalStatusDTO>>(models);
        return new ResponseDTO<ICollection<MedicalStatusDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}