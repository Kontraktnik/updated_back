using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.RelativeCQRS.Command.UpdateRelative;

public class UpdateRelativeCommand : IRequest<ResponseDTO<RelativeDTO>>
{
    public long Id { get; set; }
    public RelativeDTO model { get; set; }

    public UpdateRelativeCommand(RelativeDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}