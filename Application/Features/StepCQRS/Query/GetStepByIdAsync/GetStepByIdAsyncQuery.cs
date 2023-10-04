using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Step;
using Domain.Models.StepModels;
using MediatR;

namespace Application.Features.StepCQRS.Query.GetStepByIdAsync;

public class GetStepByIdAsyncQuery : IRequest<ResponseDTO<StepDTO>>
{
    public ISpecification<Step> specification { get; set; }

    public GetStepByIdAsyncQuery(ISpecification<Step> _specification)
    {
        specification = _specification;
    }
}