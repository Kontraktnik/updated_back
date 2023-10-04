using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.MedicalStatusCQRS.Command.UpdateMedicalStatus;

public class UpdateMedicalStatusCommandHandler : IRequestHandler<UpdateMedicalStatusCommand,ResponseDTO<MedicalStatusDTO>>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public UpdateMedicalStatusCommandHandler(IMedicalStatusRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<MedicalStatusDTO>> Handle(UpdateMedicalStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var updatedModel = _mapper.Map<MedicalStatus>(request.model);
                var model = await _repository.UpdateAsync(updatedModel);
                return new ResponseDTO<MedicalStatusDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = this.localizer["Updated"],
                    Data = _mapper.Map<MedicalStatusDTO>(model)
                };  
            }
            return new ResponseDTO<MedicalStatusDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<MedicalStatusDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}