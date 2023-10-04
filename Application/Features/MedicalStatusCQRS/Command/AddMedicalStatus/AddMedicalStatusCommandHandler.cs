using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.MedicalStatusCQRS.Command.AddMedicalStatus;

public class AddMedicalStatusCommandHandler : IRequestHandler<AddMedicalStatusCommand,ResponseDTO<MedicalStatusDTO>>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddMedicalStatusCommandHandler(IMedicalStatusRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<MedicalStatusDTO>> Handle(AddMedicalStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = _mapper.Map<MedicalStatus>(request.model);
            var newModel  = await _repository.AddAsync(model);
            return new ResponseDTO<MedicalStatusDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = this.localizer["Created"],
                Data = _mapper.Map<MedicalStatusDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<MedicalStatusDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}