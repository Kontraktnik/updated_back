using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.QualificationCQRS.Command.UpdateQualification;

public class UpdateQualificationCommandHandler : IRequestHandler<UpdateQualificationCommand,ResponseDTO<QualificationDTO>>
{
    
    private readonly IQualificationRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateQualificationCommandHandler(IQualificationRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<QualificationDTO>> Handle(UpdateQualificationCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<Qualification>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<QualificationDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<QualificationDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<QualificationDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<QualificationDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}