using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Command.UpdateRankSalary;

public class UpdateRankSalaryCommand : IRequest<ResponseDTO<RankSalaryRDTO>>
{
    public long Id { get; set; }
    public RankSalaryCUDTO model { get; set; }

    public UpdateRankSalaryCommand(RankSalaryCUDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}