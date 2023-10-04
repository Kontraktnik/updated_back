using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.PositionCQRS.Query.ListPositionAllAsync;

public class ListPositionAllAsyncQuery : IRequest<ResponseDTO<ICollection<PositionRDTO>>>
{
    
}