using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Query.ListRankSalaryAllAsync;

public class ListRankSalaryAllAsyncQuery : IRequest<ResponseDTO<ICollection<RankSalaryRDTO>>>
{
    
}