using Application.DTO.User;
using MediatR;

namespace Application.Features.SurveyExecutorCQRS.Query.GetSurveyListByRole;

public class GetSurveyListByRoleQuery : IRequest<List<long>>
{
    public UserDTO UserDto { get; set; }

    public GetSurveyListByRoleQuery(UserDTO _UserDto)
    {
        UserDto = _UserDto;
    }
    
}