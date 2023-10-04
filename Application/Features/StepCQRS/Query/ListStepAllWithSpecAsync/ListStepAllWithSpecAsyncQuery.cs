using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Step;
using Domain.Models.StepModels;
using MediatR;

namespace Application.Features.StepCQRS.Query.ListStepAllWithSpecAsync;

public class ListStepAllWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<StepDTO>>>
{
    public ISpecification<Step> specification { get; set; }

    public ListStepAllWithSpecAsyncQuery(ISpecification<Step> _specification)
    {
        specification = _specification;
    }
}