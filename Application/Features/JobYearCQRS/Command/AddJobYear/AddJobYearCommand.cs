using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.JobYearCQRS.Command.AddJobYear;

public class AddJobYearCommand : IRequest<ResponseDTO<JobYearRDTO>>
{
    public JobYearCUDTO model { get; set; }

    public AddJobYearCommand(JobYearCUDTO model)
    {
        this.model = model;
    }
}