using Application.Contracts.Specification;
using Domain.Models.VacancyModel;
using MediatR;

namespace Application.Features.VacancyCQRS.Query.CountVacancyAsync;

public class CountVacancyAsyncQuery : IRequest<int>
{
    public ISpecification<Vacancy> specification;

    public CountVacancyAsyncQuery(ISpecification<Vacancy> _specification)
    {
        specification = _specification;
    }
}