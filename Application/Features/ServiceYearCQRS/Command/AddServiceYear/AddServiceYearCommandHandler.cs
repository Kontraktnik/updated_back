using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ServiceYearCQRS.Command.AddServiceYear;

public class AddServiceYearCommandHandler : IRequestHandler<AddServiceYearCommand,ResponseDTO<ServiceYearDTO>>

{
    private readonly IServiceYearRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddServiceYearCommandHandler(IServiceYearRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ServiceYearDTO>> Handle(AddServiceYearCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<ServiceYear>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<ServiceYearDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<ServiceYearDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ServiceYearDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}