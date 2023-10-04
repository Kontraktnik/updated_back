using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ServiceYearCQRS.Command.UpdateServiceYear;

public class UpdateServiceYearCommandHandler : IRequestHandler<UpdateServiceYearCommand,ResponseDTO<ServiceYearDTO>>
{
    
    private readonly IServiceYearRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateServiceYearCommandHandler(IServiceYearRepository repository,IMapper mapper, IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ServiceYearDTO>> Handle(UpdateServiceYearCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<ServiceYear>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<ServiceYearDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<ServiceYearDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<ServiceYearDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ServiceYearDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}