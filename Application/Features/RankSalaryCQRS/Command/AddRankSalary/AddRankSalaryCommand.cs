using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Command.AddRankSalary;

public class AddRankSalaryCommand : IRequest<ResponseDTO<RankSalaryRDTO>>
{
    public RankSalaryCUDTO model { get; set; }

    public AddRankSalaryCommand(RankSalaryCUDTO model)
    {
        this.model = model;
    }
}