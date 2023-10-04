using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Vacancy;
using Domain.Models.VacancyModel;
using MediatR;

namespace Application.Features.VacancyCQRS.Query.ListVacancyWithSpecAsync;

public class ListVacancyWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<VacancyRDTO>>>
{
    public ISpecification<Vacancy> specification;

    public ListVacancyWithSpecAsyncQuery(ISpecification<Vacancy> _specification)
    {
        specification = _specification;
    }
}