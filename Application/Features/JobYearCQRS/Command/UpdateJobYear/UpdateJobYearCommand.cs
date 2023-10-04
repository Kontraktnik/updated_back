using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.JobYearCQRS.Command.UpdateJobYear;

public class UpdateJobYearCommand : IRequest<ResponseDTO<JobYearRDTO>>
{
    public long Id { get; set; }
    public JobYearCUDTO model { get; set; }

    public UpdateJobYearCommand(JobYearCUDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}