using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.Features.RankSalaryCQRS.Command.AddRankSalary;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobYearCQRS.Command.AddJobYear;

public class AddJobYearCommandHandler : IRequestHandler<AddJobYearCommand,ResponseDTO<JobYearRDTO>>

{
    private readonly IJobYearRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddJobYearCommandHandler(IJobYearRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<JobYearRDTO>> Handle(AddJobYearCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<JobYear>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<JobYearRDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<JobYearRDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<JobYearRDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}