using Application.DTO.Common;
using Application.DTO.Survey;
using MediatR;

namespace Application.Features.SurveyExecutorCQRS.Command.AddSurveyExecutor;

public class AddSurveyExecutorCommand : IRequest<ResponseDTO<SurveyExecutorDTO>>
{
    public SurveyExecutorCUDTO model;

    public AddSurveyExecutorCommand(SurveyExecutorCUDTO _model)
    {
        model = _model;
    }
}