using Application.Contracts.Specification;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Query.CountArmyDepartmentAsync;

public class CountArmyDepartmentAsyncQuery : IRequest<int>
{
    public ISpecification<ArmyDepartment> specification { get; set; }

    public CountArmyDepartmentAsyncQuery(ISpecification<ArmyDepartment> specification)
    {
        this.specification = specification;
    }
}