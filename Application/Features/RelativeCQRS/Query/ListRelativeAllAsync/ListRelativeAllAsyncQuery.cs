using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.RelativeCQRS.Query.ListRelativeAllAsync;

public class ListRelativeAllAsyncQuery : IRequest<ResponseDTO<ICollection<RelativeDTO>>>
{
    
}