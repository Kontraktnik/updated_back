using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.PositionCQRS.Command.UpdatePosition;

public class UpdatePositionCommand : IRequest<ResponseDTO<PositionRDTO>>
{
    public long Id { get; set; }
    public PositionCUDTO model { get; set; }

    public UpdatePositionCommand(PositionCUDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}