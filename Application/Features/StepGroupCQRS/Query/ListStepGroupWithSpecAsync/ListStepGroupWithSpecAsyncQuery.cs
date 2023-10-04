using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Step;
using Domain.Models.StepModels;
using MediatR;

namespace Application.Features.StepGroupCQRS.Query.ListStepGroupWithSpecAsync;

public class ListStepGroupWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<StepGroupDTO>>>
{
    public ISpecification<StepGroup> specification { get; set; }

    public ListStepGroupWithSpecAsyncQuery(ISpecification<StepGroup> specification)
    {
        this.specification = specification;
    }
}