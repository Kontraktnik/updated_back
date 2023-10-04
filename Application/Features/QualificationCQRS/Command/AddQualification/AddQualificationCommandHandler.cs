using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.QualificationCQRS.Command.AddQualification;

public class AddQualificationCommandHandler : IRequestHandler<AddQualificationCommand,ResponseDTO<QualificationDTO>>

{
    private readonly IQualificationRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddQualificationCommandHandler(IQualificationRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<QualificationDTO>> Handle(AddQualificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<Qualification>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<QualificationDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<QualificationDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<QualificationDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}