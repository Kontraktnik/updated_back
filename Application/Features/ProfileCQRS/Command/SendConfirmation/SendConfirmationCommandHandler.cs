using System.Diagnostics;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.User;
using Application.Helpers;
using Application.Resource;
using AutoMapper;
using Domain.Models.ProfileModels;
using MediatR;
using Microsoft.Extensions.Localization;
using Profile = Domain.Models.ProfileModels.Profile;

namespace Application.Features.ProfileCQRS.Command.SendConfirmation;

public class SendConfirmationCommandHandler : IRequestHandler<SendConfirmationCommand,ResponseDTO<ProfileDTO>>
{
    public SendConfirmationCommandHandler(IMapper mapper, IProfileRepository profileRepository, IUserRepository userRepository, ISurveyRepository surveyRepository, IStepRepository stepRepository, IStepOrderRepository stepOrderRepository, ISurveyExecutorRepository surveyExecutorRepository,
        IStringLocalizer<Localize> localizer)
    {
        _mapper = mapper;
        _profileRepository = profileRepository;
        _userRepository = userRepository;
        _surveyRepository = surveyRepository;
        _stepRepository = stepRepository;
        _stepOrderRepository = stepOrderRepository;
        _surveyExecutorRepository = surveyExecutorRepository;
        _localizer = localizer;
    }

    private readonly IMapper _mapper;
    private readonly IProfileRepository _profileRepository;
    private readonly IUserRepository _userRepository;
    private readonly  ISurveyRepository _surveyRepository;
    private readonly IStepRepository _stepRepository;
    private readonly IStepOrderRepository _stepOrderRepository;
    private readonly ISurveyExecutorRepository _surveyExecutorRepository;
    private readonly IStringLocalizer<Localize> _localizer;

    public async Task<ResponseDTO<ProfileDTO>> Handle(SendConfirmationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Определим основные переменные
            UserDTO currentUser = request.CurrentUser;
            SendConfirmationDTO model = request.model;
            long ProfileId = request.ProfileId;
            //Проверим наличие заявки на подтверждение
            var profile = await _profileRepository.GetByIdAsync(ProfileId);
            if (profile == null)
            {
                return returnErrorStatus(false, 404, _localizer["Forbidden"]);
            }
            //Мы можем отправить запрос только если статус равен нулю
            if (profile.Status != 0 || profile.RequestedStatus == -1 || profile.RequestedStatus == 0 || profile.RequestedStatus == null || profile.ConfirmedStatus == 1 || profile.ConfirmedStatus == -1)
            {
                return returnErrorStatus(false, 400, _localizer["Forbidden"]);
            }

            //Найдем данный этап
            var currentStep = await _stepRepository.GetByIdAsync(profile.StepId);
            if (currentStep == null)
            {
                return returnErrorStatus(false, 400, _localizer["NotFound"] + ":" + _localizer["Step"]);
            }
            //Найдем очередность данного этапа
            var stepOrder = await _stepOrderRepository.getByStepId(currentStep.Id);
            if (stepOrder == null)
            {
                return returnErrorStatus(false, 400, _localizer["NotFound"] + ":" + _localizer["StepOrder"]);
            }
            //Найдем данную анкету
            var survey = await _surveyRepository.GetByIdAsync(profile.SurveyId);
            if (survey == null)
            {
                return returnErrorStatus(false, 400, _localizer["NotFound"] +":" +_localizer["Survey"]);
            }
            //Проверим имеет ли право данный пользователь на данную заявку
            //Если ответственное лицо за подтверждение пользователь руководитель или исполнитель
            var executors = await _surveyExecutorRepository.GetBySurveyId(survey.Id);
            if (executors == null)
            {
                return returnErrorStatus(false, 404, _localizer["NotFound"] +":" +_localizer["SurveyExecutor"]);
            }

            //Если директор
            if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.DirectorRoleId))
            {
                if (executors.DirectorId != currentUser.Id)
                {
                    return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                }
            }

            //Если Исполнитель
            if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
            {
                if (executors.ExecutorId != currentUser.Id)
                {
                    return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                }
            }

            //Если пользователь
            if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.UserRoleId))
            {
                if (survey.UserId != currentUser.Id)
                {
                    return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                }
            }

            //Другие роли исключительно по региону
            if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.MEDRoleId) ||
                currentStep.ConfirmedRoleId.Equals(ValidationHelpers.KNBRoleId))
            {
                if (survey.BirthAreaId != currentUser.AreaId)
                {
                    return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                }
            }

            //После проверки прав подтверждаем уровень
            profile.ConfirmedUserId = currentUser.Id;
            profile.ConfirmedUserIIN = currentUser.IIN;
            profile.Comment = model.Comment;
            profile.ConfirmedStatus = model.Status;
            profile.Status = model.Status;
            profile.UpdatedAt = DateTime.Now;
            survey.Status = model.Status;
            //Если уровень мед проверки
            if (currentStep.Id.Equals(ValidationHelpers.MedState))
            {
                survey.MedicalStatusId = model.MedicalStatusId;
            }
            //Подтверждающий дал разрешение - передаем на следующий уровень
            if (model.Status.Equals(1) && !currentStep.IsLast)
            {
                var next_step = await _stepRepository.GetByIdAsync(stepOrder.NextStepId ?? 0);
                if (next_step == null)
                {
                    return returnErrorStatus(false, 400, _localizer["NotFound"] + ":" + _localizer["StepOrders"]);
                }

                //Создаем следующие запросы
                var newProfile = new Profile
                {
                    StepId = next_step.Id,
                    StepGroupId = next_step.StepGroupId,
                    SurveyId = survey.Id,
                    RequestedStatus = 0,
                    RequestedSIGN = String.Empty,
                    ConfirmedStatus = 0,
                    AreaId = survey.BirthAreaId,
                    Status = 0,
                    CreatedAt = DateTime.Now,
                    ExpiredAt = DateTime.Now.AddDays(next_step.DayLimit)
                };
                if (next_step.RequestedRoleId.Equals(ValidationHelpers.DirectorRoleId))
                {
                    var director = await _userRepository.GetByIdAsync(executors.DirectorId);
                    newProfile.RequestedUserId = director.Id;
                    newProfile.RequestedUserIIN = director.IIN;
                }

                else if (next_step.RequestedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
                {
                    var executor = await _userRepository.GetByIdAsync(executors.ExecutorId);
                    newProfile.RequestedUserId = executor.Id;
                    newProfile.RequestedUserIIN = executor.IIN;
                }

                else if (next_step.RequestedRoleId.Equals(ValidationHelpers.UserRoleId))
                {
                    var userExecutor = await _userRepository.GetByIdAsync(survey.UserId);
                    newProfile.RequestedUserId = userExecutor.Id;
                    newProfile.RequestedUserIIN = userExecutor.IIN;
                }
                else
                {
                    return returnErrorStatus(false, 400, _localizer["NotFound"] + ":" + _localizer["SurveyExecutors"]);
                }
                //Если подтверждающее лицо тоже что и дающее заявку
                if (currentUser.Id == newProfile.RequestedUserId && next_step.Id != ValidationHelpers.SpecialState)
                {
                    newProfile.RequestedStatus = 1;
                    
                    if (next_step.ConfirmedRoleId.Equals(ValidationHelpers.DirectorRoleId))
                    {
                        var director = await _userRepository.GetByIdAsync(executors.DirectorId);
                        newProfile.ConfirmedUserId = director.Id;
                        newProfile.ConfirmedUserIIN = director.IIN;
                    }

                    else if (next_step.ConfirmedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
                    {
                        var executor = await _userRepository.GetByIdAsync(executors.ExecutorId);
                        newProfile.ConfirmedUserId = executor.Id;
                        newProfile.ConfirmedUserIIN = executor.IIN;
                    }

                    else if (next_step.ConfirmedRoleId.Equals(ValidationHelpers.UserRoleId))
                    {
                        var userExecutor = await _userRepository.GetByIdAsync(survey.UserId);
                        newProfile.ConfirmedUserId = userExecutor.Id;
                        newProfile.ConfirmedUserIIN = userExecutor.IIN;
                    }
                    
                }
                survey.StepGroupId = next_step.StepGroupId;
                survey.CurrentStepId = next_step.Id;
                survey.Status = 0;
                survey.UpdatedAt = DateTime.Now;
                await _profileRepository.AddAsync(newProfile);
            }
                
                profile = await _profileRepository.UpdateAsync(profile);
                await _surveyRepository.UpdateAsync(survey);
            

            
            return new ResponseDTO<ProfileDTO>()
            {
                Success = true,
                StatusCode = 200,
                Message = _localizer["Updated"],
                Data = _mapper.Map<ProfileDTO>(profile)
            };
        }
        catch (Exception ex)
        {
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();
            return returnErrorStatus(false, 500, ex.ToString());
        }

    }
    
    private ResponseDTO<ProfileDTO> returnErrorStatus(bool Success,int Code, string Message)
    {
        return new ResponseDTO<ProfileDTO>()
        {
            Success = Success,
            StatusCode = Code,
            Message = Message,
            
        };
    }

}