using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Survey;
using AutoMapper;
using MediatR;

namespace Application.Features.SurveyCQRS.Query.ListSurveyWithSpecAsync;

public class ListSurveyWithSpecAsyncQueryHandler : IRequestHandler<ListSurveyWithSpecAsyncQuery,ResponseDTO<ICollection<SurveyDTO>>>
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ISurveyExecutorRepository _surveyExecutorRepository;
    private IMapper _mapper;
    public ListSurveyWithSpecAsyncQueryHandler(IMapper mapper, ISurveyRepository surveyRepository,ISurveyExecutorRepository surveyExecutorRepository)
    {
        _surveyRepository = surveyRepository;
        _surveyExecutorRepository = surveyExecutorRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<SurveyDTO>>> Handle(ListSurveyWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var result = await _surveyRepository.ListWithSpecAsync(request.specification);
        return new ResponseDTO<ICollection<SurveyDTO>>
        {
            Success = true,
            StatusCode = (int) HttpStatusCode.OK,
            Data = _mapper.Map<ICollection<SurveyDTO>>(result)
        };
    }
}