using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobYearCQRS.Command.UpdateJobYear;

public class UpdateJobYearCommandCommandHandler : IRequestHandler<UpdateJobYearCommand,ResponseDTO<JobYearRDTO>>
{
    
    private readonly IJobYearRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateJobYearCommandCommandHandler(IJobYearRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<JobYearRDTO>> Handle(UpdateJobYearCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<JobYear>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<JobYearRDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<JobYearRDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<JobYearRDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<JobYearRDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}