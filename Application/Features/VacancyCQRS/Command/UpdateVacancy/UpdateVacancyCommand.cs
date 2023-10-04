using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Vacancy;
using Domain.Models.UserModels;
using MediatR;

namespace Application.Features.VacancyCQRS.Command.UpdateVacancy;

public class UpdateVacancyCommand : IRequest<ResponseDTO<VacancyRDTO>>
{
    public VacancyCUDTO model;
    public ISpecification<User> specification;
    public long Id { get; set; }
    
    public UpdateVacancyCommand(VacancyCUDTO _model,ISpecification<User> _specification,long _Id)
    {
        model = _model;
        specification = _specification;
        Id = _Id;
    }
}