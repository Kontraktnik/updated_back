using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;

public class GetSurveyForEmailWithSpecAsyncQueryHandler : IRequestHandler<GetSurveyForEmailWithSpecAsyncQuery,ResponseDTO<SurveyDTO>>
{
    private readonly ISurveyRepository _surveyRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetSurveyForEmailWithSpecAsyncQueryHandler(IMapper mapper, ISurveyRepository surveyRepository,ISurveyExecutorRepository surveyExecutorRepository, IStringLocalizer<Localize> localizer)
    {
        _surveyRepository = surveyRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<SurveyDTO>> Handle(GetSurveyForEmailWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
         try
        {
            var specification = request.specification;
            var survey = await _surveyRepository.GetEntityWithSpecAsync(specification);
            if (survey != null)
            {
                return new ResponseDTO<SurveyDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.OK,
                    Data = _mapper.Map<SurveyDTO>(survey)
                };
            }
            else
            {
                return new ResponseDTO<SurveyDTO>
                {
                    Success = false,
                    StatusCode = (int) HttpStatusCode.NotFound,
                    Message = this.localizer["NotFound"]
                };
            }
            
        }
        catch (Exception ex)
        {
            return new ResponseDTO<SurveyDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}