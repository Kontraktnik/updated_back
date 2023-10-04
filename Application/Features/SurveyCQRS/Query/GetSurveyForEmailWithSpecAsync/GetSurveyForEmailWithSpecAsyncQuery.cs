using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Survey;
using Domain.Models.SurveyModels;
using MediatR;

namespace Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;

public class GetSurveyForEmailWithSpecAsyncQuery : IRequest<ResponseDTO<SurveyDTO>>
{
    public ISpecification<Survey> specification { get; set; }

    public GetSurveyForEmailWithSpecAsyncQuery(ISpecification<Survey> _specification)
    {
        specification = _specification;
    }
}