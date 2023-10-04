using Application.DTO.Common;
using Application.DTO.Survey;
using MediatR;

namespace Application.Features.SurveyCQRS.Command.AddSurvey;

public class AddSurveyCommand : IRequest<ResponseDTO<SurveyDTO>>
{
    public SurveyCUDTO model;
    public string IIN;
    public int? Status;

    public AddSurveyCommand(SurveyCUDTO _model, string iin,int? Status = null)
    {
        model = _model;
        IIN = iin;
        this.Status = Status;
    }


}