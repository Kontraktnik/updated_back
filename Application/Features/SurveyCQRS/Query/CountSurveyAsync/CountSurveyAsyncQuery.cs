using Application.Contracts.Specification;
using Domain.Models.SurveyModels;
using MediatR;

namespace Application.Features.SurveyCQRS.Query.CountSurveyAsync;

public class CountSurveyAsyncQuery : IRequest<int>
{
    public ISpecification<Survey> specification;

    public CountSurveyAsyncQuery(ISpecification<Survey> specification)
    {
        this.specification = specification;
    }
}