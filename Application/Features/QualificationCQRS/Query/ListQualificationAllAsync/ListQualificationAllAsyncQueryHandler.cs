using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.ListQualificationAllAsync;

public class ListQualificationAllAsyncQueryHandler : IRequestHandler<ListQualificationAllAsyncQuery,ResponseDTO<ICollection<QualificationDTO>>>
{
    private readonly IQualificationRepository _repository;
    private readonly IMapper _mapper;

    public ListQualificationAllAsyncQueryHandler(IQualificationRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<QualificationDTO>>> Handle(ListQualificationAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<QualificationDTO>>(models);
        return new ResponseDTO<ICollection<QualificationDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}