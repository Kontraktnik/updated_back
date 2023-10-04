using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.JobYearCQRS.Query.ListJobYearAllAsync;

public class ListJobYearAllAsyncQuery : IRequest<ResponseDTO<ICollection<JobYearRDTO>>>
{
    
}