using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.RelativeCQRS.Command.AddRelative;

public class AddRelativeCommand : IRequest<ResponseDTO<RelativeDTO>>
{
    public RelativeDTO model { get; set; }

    public AddRelativeCommand(RelativeDTO model)
    {
        this.model = model;
    }
}