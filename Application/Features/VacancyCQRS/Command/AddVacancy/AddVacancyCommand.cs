using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Vacancy;
using Domain.Models.UserModels;
using MediatR;

namespace Application.Features.VacancyCQRS.Command.AddVacancy;

public class AddVacancyCommand : IRequest<ResponseDTO<VacancyRDTO>>
{
    public VacancyCUDTO model;
    public ISpecification<User> specification;

    public AddVacancyCommand(VacancyCUDTO _model,ISpecification<User> _specification)
    {
        model = _model;
        specification = _specification;
    }


}