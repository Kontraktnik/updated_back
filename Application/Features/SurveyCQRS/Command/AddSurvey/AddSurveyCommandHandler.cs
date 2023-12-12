using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.Resource;
using AutoMapper;
using Domain.Models.DigitalSignModels;
using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Command.AddSurvey;

public class AddSurveyCommandHandler : IRequestHandler<AddSurveyCommand,ResponseDTO<SurveyDTO>>
{
    private ISurveyRepository _surveyRepository;
    private IUserRepository _userRepository;
    private IVacancyRepository _vacancyRepository;
    private ISurveyDriverRepository _surveyDriverRepository;
    private ISurveyRelativeRepository _surveyRelativeRepository;
    private IPositionRepository _positionRepository;
    private IEducationRepository _educationRepository;
    private IAreaRepository _areaRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer { get; set; }
    public AddSurveyCommandHandler(ISurveyRepository surveyRepository,
        IUserRepository userRepository,
        IMapper mapper,IVacancyRepository vacancyRepository,
        ISurveyDriverRepository surveyDriverRepository,
         ISurveyRelativeRepository surveyRelativeRepository,
        IPositionRepository positionRepository,
        IEducationRepository educationRepository,
        IAreaRepository areaRepository,
        IStringLocalizer<Localize> localizer
    )
    {
        _surveyRepository = surveyRepository;
        _userRepository = userRepository;
        _vacancyRepository = vacancyRepository;
        _mapper = mapper;
        _surveyDriverRepository = surveyDriverRepository;
        _surveyRelativeRepository = surveyRelativeRepository;
        _positionRepository = positionRepository;
        _areaRepository = areaRepository;
        _educationRepository = educationRepository;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<SurveyDTO>> Handle(AddSurveyCommand request, CancellationToken cancellationToken)
    {

        try
        {

            var user = await _userRepository.getUserByIIN(request.IIN);
            if (user != null)
            {

                if ((await _positionRepository.GetByIdAsync(request.model.PositionId)).Equals(null))
                {
                    return new ResponseDTO<SurveyDTO>()
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.BadRequest,
                        Message = $"{this.localizer["NotFound"]}:{this.localizer["Position"]}"
                    };
                }
                
                if ((await _educationRepository.GetByIdAsync(request.model.EducationId)).Equals(null))
                {
                    return new ResponseDTO<SurveyDTO>()
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.BadRequest,
                        Message = $"{this.localizer["NotFound"]}:{this.localizer["Education"]}"
                    };
                }
                if ((await _areaRepository.GetByIdAsync(request.model.AreaId)).Equals(null) || (await _areaRepository.GetByIdAsync(request.model.BirthAreaId)).Equals(null))
                {
                    return new ResponseDTO<SurveyDTO>()
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.BadRequest,
                        Message = $"{this.localizer["NotFound"]}:{this.localizer["Area"]}"
                    };
                }

                if (request.model.VacancyId != null)
                {
                    if (!(await _surveyRepository.GetSurveyByVacancyIdAndUserId(user.Id, request.model.VacancyId ?? -1))
                        .Equals(null))
                    {
                        return new ResponseDTO<SurveyDTO>()
                        {
                            Success = false,
                            StatusCode = (int) HttpStatusCode.BadRequest,
                            Message = $"{this.localizer["Forbidden"]}"
                        };
                    }
                }
                
                //Savings .....
                var survey = _mapper.Map<Survey>(request.model);
                survey.UserId = user.Id;
                survey.IIN = user.IIN;
                survey.FullName = user.FullName;
                survey.Email = user.Email;
                survey.Phone = user.Phone;
                survey.CreatedAt = DateTime.Now;
                survey.UpdatedAt = DateTime.Now;
                // survey.DigitalSigns = new List<DigitalSign>()
                // {
                //     new DigitalSign()
                //     {
                //         FileName = request.model.UserSign, Signed = DateTime.Now, IsValid = true,
                //         WhoSignedId = user.Id
                //     }
                // };

                if (request.Status != null)
                {
                    survey.CurrentStepId = 1;
                    survey.StepGroupId = 1;
                }
                //Set Vacancies
                if (request.model.VacancyId.HasValue)
                {
                    var vacancy = await _vacancyRepository.GetByIdAsync(request.model.VacancyId??0);
                    if (vacancy != null)
                    {
                        survey.AreaId = vacancy.AreaId;
                    }
                }

                var newSurvey = await _surveyRepository.AddAsync(survey);
                
                if (request.model.DriverLicense != null && request.model.DriverLicense.Count > 0)
                {
                    foreach (var driverId in request.model.DriverLicense)
                    {
                        await _surveyDriverRepository.AddAsync(
                            new SurveyDriver()
                            {
                                SurveyId = newSurvey.Id,
                                DriverLicenseId = driverId
                            }
                        );
                    }
                }
                if (request.model.Relatives != null && request.model.Relatives.Count > 0)
                {
                    foreach (var relative in request.model.Relatives)
                    {
                        var relativeModel = _mapper.Map<SurveyRelative>(relative);
                        relativeModel.SurveyId = newSurvey.Id;
                        await _surveyRelativeRepository.AddAsync(
                            relativeModel
                        );
                    }
                }
                return new ResponseDTO<SurveyDTO>()
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = $"{this.localizer["Created"]}",
                    Data = _mapper.Map<SurveyDTO>(newSurvey)
                };
                
            }
            else
            {
                return new ResponseDTO<SurveyDTO>()
                {
                    Success = false,
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Message = $"{this.localizer["NotFound"]}:{this.localizer["User"]}"
                };
            }
            
        }
        catch (Exception ex)
        {
            return new ResponseDTO<SurveyDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.ToString()
            };
        }
        
    }
}