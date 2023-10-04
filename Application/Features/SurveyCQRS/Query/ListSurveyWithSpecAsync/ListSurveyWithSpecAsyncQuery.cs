using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.DTO.User;
using Domain.Models.SurveyModels;
using MediatR;

namespace Application.Features.SurveyCQRS.Query.ListSurveyWithSpecAsync;

public class ListSurveyWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<SurveyDTO>>>
{
    public ISpecification<Survey> specification;
    public UserDTO UserDto;

    public ListSurveyWithSpecAsyncQuery(ISpecification<Survey> _specification,UserDTO _UserDto)
    {
        specification = _specification;
        UserDto = _UserDto;
    }
}