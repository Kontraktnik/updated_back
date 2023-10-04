using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Vacancy;
using Domain.Models.VacancyModel;
using MediatR;

namespace Application.Features.VacancyCQRS.Query.GetVacancyByIdAsync;

public class GetVacancyByIdAsyncQuery :  IRequest<ResponseDTO<VacancyRDTO>>
{
    public ISpecification<Vacancy> specification;

    public GetVacancyByIdAsyncQuery(ISpecification<Vacancy> _specification)
    {
        specification = _specification;
    }
}