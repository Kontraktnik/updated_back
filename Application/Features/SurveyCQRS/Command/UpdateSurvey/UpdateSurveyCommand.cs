using Application.DTO.Common;
using Application.DTO.Survey;
using Application.DTO.User;
using MediatR;

namespace Application.Features.SurveyCQRS.Command.UpdateSurvey;

public class UpdateSurveyCommand : IRequest<ResponseDTO<bool>>
{
    public UpdateSurveyCommand(UserDTO user, long surveyId, SurveyMedDTO model)
    {
        this.user = user;
        SurveyId = surveyId;
        this.model = model;
    }

    public UserDTO user;
    public long SurveyId;
    public SurveyMedDTO model;





}