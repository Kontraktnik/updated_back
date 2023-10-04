using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RelativeCQRS.Query.GetRelativeByIdAsync;

public class GetRelativeByIdAsyncQuery : IRequest<ResponseDTO<RelativeDTO>>
{
    public  long Id { get; set; }

    public GetRelativeByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}