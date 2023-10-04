using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Step;
using Domain.Models.StepModels;
using MediatR;

namespace Application.Features.StepGroupCQRS.Query.GetStepGroupWithSpecAsync;

public class GetStepGroupWithSpecAsyncQuery : IRequest<ResponseDTO<StepGroupDTO>>
{
    public ISpecification<StepGroup> specification { get; set; }

    public GetStepGroupWithSpecAsyncQuery(ISpecification<StepGroup> specification)
    {
        this.specification = specification;
    }
}