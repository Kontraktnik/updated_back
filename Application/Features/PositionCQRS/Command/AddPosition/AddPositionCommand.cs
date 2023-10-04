using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.PositionCQRS.Command.AddPosition;

public class AddPositionCommand : IRequest<ResponseDTO<PositionRDTO>>
{
    public PositionCUDTO model { get; set; }

    public AddPositionCommand(PositionCUDTO model)
    {
        this.model = model;
    }
}