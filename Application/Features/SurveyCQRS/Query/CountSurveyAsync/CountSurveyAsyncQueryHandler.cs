using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.SurveyCQRS.Query.CountSurveyAsync;

public class CountSurveyAsyncQueryHandler : IRequestHandler<CountSurveyAsyncQuery,int>
{
    private readonly ISurveyRepository _surveyRepository;
    private IMapper _mapper;
    
    public CountSurveyAsyncQueryHandler(IMapper mapper, ISurveyRepository surveyRepository)
    {
        _surveyRepository = surveyRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountSurveyAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _surveyRepository.CountAsync(request.specification);
    }
}