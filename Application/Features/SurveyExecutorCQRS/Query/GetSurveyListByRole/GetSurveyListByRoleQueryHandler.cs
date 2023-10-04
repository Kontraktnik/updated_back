using Application.Contracts.Persistence;
using Application.Helpers;
using Application.Resource;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyExecutorCQRS.Query.GetSurveyListByRole;

public class GetSurveyListByRoleQueryHandler : IRequestHandler<GetSurveyListByRoleQuery,List<long>>
{
    public GetSurveyListByRoleQueryHandler(ISurveyExecutorRepository surveyExecutorRepository,IStringLocalizer<Localize> localizer)
    {
        _surveyExecutorRepository = surveyExecutorRepository;
        this.localizer = localizer;
    }

    private ISurveyExecutorRepository _surveyExecutorRepository;
    private IStringLocalizer<Localize> localizer;

    public async Task<List<long>> Handle(GetSurveyListByRoleQuery request, CancellationToken cancellationToken)
    {
        var user = request.UserDto;
        if (user.RoleId.Equals(ValidationHelpers.DirectorRoleId))
        {
            return await _surveyExecutorRepository.GetDirectorSurvey(user.Id);
        }
        if (user.RoleId.Equals(ValidationHelpers.ExecutorRoleId))
        {
            return await _surveyExecutorRepository.GetExecutorsSurvey(user.Id);
        }
        return new List<long>();
    }
}