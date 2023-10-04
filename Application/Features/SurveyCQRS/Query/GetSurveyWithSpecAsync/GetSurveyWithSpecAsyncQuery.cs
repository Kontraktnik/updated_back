using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.DTO.User;
using Domain.Models.SurveyModels;
using MediatR;

namespace Application.Features.SurveyCQRS.Query.GetSurveyWithSpecAsync;

public class GetSurveyWithSpecAsyncQuery : IRequest<ResponseDTO<SurveyDTO>>
{
    public ISpecification<Survey> specification { get; set; }
    public UserDTO UserDto { get; set; }

    public GetSurveyWithSpecAsyncQuery(ISpecification<Survey> _specification, UserDTO userDto)
    {
        specification = _specification;
        UserDto = userDto;
    }
    
    
}