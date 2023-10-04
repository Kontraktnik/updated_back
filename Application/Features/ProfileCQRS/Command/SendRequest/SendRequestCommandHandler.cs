using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.Step;
using Application.DTO.User;
using Application.Helpers;
using Application.Resource;
using AutoMapper;
using Domain.Models.ProfileModels;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.UserModels;
using MediatR;
using Microsoft.Extensions.Localization;
using Profile = Domain.Models.ProfileModels.Profile;

namespace Application.Features.ProfileCQRS.Command.SendRequest;

public class SendRequestCommandHandler : IRequestHandler<SendRequestCommand,ResponseDTO<ProfileDTO>>
{
    
    public SendRequestCommandHandler(IMapper mapper, IProfileRepository profileRepository, IUserRepository userRepository, ISurveyRepository surveyRepository, IStepRepository stepRepository, IStepOrderRepository stepOrderRepository, ISurveyExecutorRepository surveyExecutorRepository,
           IStringLocalizer<Localize> localizer
        )
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


    
    public async Task<ResponseDTO<ProfileDTO>> Handle(SendRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
        //При отправке запроса инициализируем две переменные
        UserDTO currentUser = request.user;
        SendRequestDTO model = request.model;
        //Проверяем наличие этапа
        var currentStep = await _stepRepository.GetByIdAsync(model.StepId);
        if (currentStep.Equals(null))
        {
            return returnErrorStatus(false, 400, _localizer["NotFound"] + ":" + _localizer["Step"]);
        }
        //Получаем очередность этапов
        var stepOrder = await _stepOrderRepository.getByStepId(currentStep.Id);
        if (stepOrder.Equals(null))
        {
            return returnErrorStatus(false, 404, _localizer["NotFound"] + ":" + _localizer["StepOrder"]);
        }
        //Проверяем наличие анкеты
        var survey = await _surveyRepository.GetByIdAsync(model.SurveyId);
        if (survey.Equals(null))
        {
            return returnErrorStatus(false, 404, _localizer["NotFound"] + ":" + _localizer["Survey"]);
        }
        //Если заявка уже оформлена 1 -1 и текущий этап уже назначен то нет смысла продолжать работать
        if (survey.Status != 0 || survey.CurrentStepId > currentStep.Id)
        {
            return returnErrorStatus(false, 403, _localizer["Forbidden"] );
        }
        //Проверяем имеет ли данный запрос идти от имени данного пользователя всего их 3 Руководитель Исполнитель и Пользователь
        
        //Распределенные роли доступны после 1 этапа
        var executors = await _surveyExecutorRepository.GetBySurveyId(survey.Id);
        
        if (currentStep.RequestedRoleId.Equals(ValidationHelpers.DirectorRoleId))
        {
            //Руководитель не найден доступа нет
            if (executors.DirectorId != currentUser.Id)
            {
                return returnErrorStatus(false, 403, _localizer["Forbidden"]);
            }
            
        }
        if (currentStep.RequestedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
        {
            //Руководитель не найден доступа нет
            if (executors.ExecutorId != currentUser.Id)
            {
                return returnErrorStatus(false, 403, _localizer["Forbidden"]);
            }
        }
        if (currentStep.RequestedRoleId.Equals(ValidationHelpers.UserRoleId))
        {
            //Пользователю не принадлежит данная анкета
            if (survey.UserId != currentUser.Id)
            {
                return returnErrorStatus(false, 403, _localizer["Forbidden"]);
            }
            
        }
        //Проверяем есть ли профайл с такими данными как этап и уровень если нет то создаем ее
        var profile = await _profileRepository.GetBySurveyAndStep(survey.Id, model.StepId);
        //Профайл не создан надо лишь создать для дальнейшей работы
        if (profile == null && currentStep.IsFirst)
        {
            Profile newProfile = createNewProfile(survey, model, currentStep, currentUser);
            survey.Status = model.Status == -1 ?survey.Status = -1 : 0;
            survey.StepGroupId = currentStep.StepGroupId;
            survey.CurrentStepId = currentStep.Id;
            //Меняем заявку и отправляем первую заявку
            await _profileRepository.AddAsync(newProfile);
            await _surveyRepository.UpdateAsync(survey);
            return new ResponseDTO<ProfileDTO>()
            {
                Success = true,
                StatusCode = 200,
                Message = _localizer["Updated"],
                Data = _mapper.Map<ProfileDTO>(profile)
            };
        }
        if (executors.Equals(null))
        {
            return returnErrorStatus(false, 403, _localizer["Forbidden"]);
        }
        //Проверяем предыдущий этап
        var prevProfile = await _profileRepository.GetBySurveyAndStep(survey.Id, stepOrder.PreviousStepId??0);
        if (prevProfile == null)
        {
            return returnErrorStatus(false, 404, _localizer["NotFound"] + ":" + _localizer["PreviousStepId"]);
        }
        if (prevProfile.Status != 1)
        {
            return returnErrorStatus(false, 403, _localizer["Forbidden"]);
        }
        
        //Промежуточный этап
        else if (!currentStep.IsFirst && !currentStep.IsLast)
        {
            var next_step = await _stepRepository.GetByIdAsync(stepOrder.NextStepId??0);
            if (next_step == null)
            {
                return returnErrorStatus(false, 403, _localizer["NotFound"] + ":" + _localizer["NextStepId"]);
            }
            //Если заявку создает само ответственное лицо
            if (profile == null)
            {
                //Предыдущий профиль прошел успешно необходимо создать новый профиль
                Profile newProfile = createNewProfile(survey, model, currentStep, currentUser);
                newProfile.ExpiredAt = DateTime.Now.AddDays(currentStep.DayLimit);
                survey.Status = model.Status == -1 ?survey.Status = -1 : 0;
                survey.StepGroupId = currentStep.StepGroupId;
                survey.CurrentStepId = currentStep.Id;
                survey.UpdatedAt = DateTime.Now;
                //Ищем следующего исполнителя
                if (model.Status.Equals(1))
                {
                    //Если следующая роль директора
                    if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.DirectorRoleId))
                    {
                        var director = await _userRepository.GetByIdAsync(executors.DirectorId);
                        newProfile.ConfirmedUserId = director.Id;
                        newProfile.ConfirmedUserIIN = director.IIN;
                    }
                    //Если следующая роль исполнителя
                    else if(currentStep.ConfirmedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
                    {
                        var executorUser = await _userRepository.GetByIdAsync(executors.ExecutorId);
                        newProfile.ConfirmedUserId = executorUser.Id;
                        newProfile.ConfirmedUserIIN = executorUser.IIN;
                    }
                    //Если следующая роль пользователя
                    else if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.UserRoleId))
                    {
                        var User = await _userRepository.GetByIdAsync(survey.UserId);
                        newProfile.ConfirmedUserId = User.Id;
                        newProfile.ConfirmedUserIIN = User.IIN;
                    }
                }
                newProfile = await _profileRepository.AddAsync(newProfile);
                await _surveyRepository.UpdateAsync(survey);
                return new ResponseDTO<ProfileDTO>()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = _localizer["Updated"],
                    Data = _mapper.Map<ProfileDTO>(newProfile)
                };
            }
            //Заявка создана при помощи подтверждения
            else
            {
                //Проверяем текущий статус заявки
                if (profile.RequestedStatus.Equals(0) && profile.Status.Equals(0))
                {
                    if (profile.RequestedUserId != currentUser.Id)
                    {
                        return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                    }
                    profile.RequestedSIGN = model.SignKey;
                    profile.RequestedStatus = model.Status;
                    if (model.Status == -1)
                    {
                        survey.Status = -1;
                        profile.Status = -1;
                    }
                    //Ищем следующего исполнителя
                    if (model.Status.Equals(1))
                    {
                        //Если следующая роль директора
                        if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.DirectorRoleId))
                        {
                            var director = await _userRepository.GetByIdAsync(executors.DirectorId);
                            profile.ConfirmedUserId = director.Id;
                            profile.ConfirmedUserIIN = director.IIN;
                        }
                        //Если следующая роль исполнителя
                        else if(currentStep.ConfirmedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
                        {
                            var executorUser = await _userRepository.GetByIdAsync(executors.ExecutorId);
                            profile.ConfirmedUserId = executorUser.Id;
                            profile.ConfirmedUserIIN = executorUser.IIN;
                        }
                        //Если следующая роль пользователя
                        else if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.UserRoleId))
                        {
                            var User = await _userRepository.GetByIdAsync(survey.UserId);
                            profile.ConfirmedUserId = User.Id;
                            profile.ConfirmedUserIIN = User.IIN;
                        }
                    }
                    profile.UpdatedAt = DateTime.Now;
                    survey.UpdatedAt = DateTime.Now;
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
                else
                {
                    return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                }
            }
        }

        //Последний этап
        else if (currentStep.IsLast)
        {
            //Если заявку создает само ответственное лицо
            if (profile == null)
            {
                //Предыдущий профиль прошел успешно необходимо создать новый профиль
                Profile newProfile = createNewProfile(survey, model, currentStep, currentUser);
                newProfile.CreatedAt = DateTime.Now;
                newProfile.ExpiredAt = DateTime.Now;
                survey.UpdatedAt = DateTime.Now;
                survey.Status = model.Status == -1 ?survey.Status = -1 : 0;
                survey.StepGroupId = currentStep.StepGroupId;
                survey.CurrentStepId = currentStep.Id;
                //Ищем следующего исполнителя
                if (model.Status.Equals(1))
                {
                    //Если следующая роль директора
                    if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.DirectorRoleId))
                    {
                        var director = await _userRepository.GetByIdAsync(executors.DirectorId);
                        newProfile.ConfirmedUserId = director.Id;
                        newProfile.ConfirmedUserIIN = director.IIN;
                    }
                    //Если следующая роль исполнителя
                    else if(currentStep.ConfirmedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
                    {
                        var executorUser = await _userRepository.GetByIdAsync(executors.ExecutorId);
                        newProfile.ConfirmedUserId = executorUser.Id;
                        newProfile.ConfirmedUserIIN = executorUser.IIN;
                    }
                    //Если следующая роль пользователя
                    else if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.UserRoleId))
                    {
                        var User = await _userRepository.GetByIdAsync(survey.UserId);
                        newProfile.ConfirmedUserId = User.Id;
                        newProfile.ConfirmedUserIIN = User.IIN;
                    }
                }
                newProfile = await _profileRepository.AddAsync(newProfile);
                await _surveyRepository.UpdateAsync(survey);
                return new ResponseDTO<ProfileDTO>()
                {
                    Success = true,
                    StatusCode = 200,
                    Message = _localizer["Updated"],
                    Data = _mapper.Map<ProfileDTO>(newProfile)
                };
            }
            //Заявка создана при помощи подтверждения
            else
            {
                //Проверяем текущий статус заявки
                if (profile.RequestedStatus.Equals(0) && profile.Status.Equals(0))
                {
                    if (profile.RequestedUserId != currentUser.Id)
                    {
                        return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                    }
                    profile.RequestedSIGN = model.SignKey;
                    profile.RequestedStatus = model.Status;
                    if (model.Status == -1)
                    {
                        survey.Status = -1;
                        profile.Status = -1;
                    }
                    //Ищем следующего исполнителя
                    if (model.Status.Equals(1))
                    {
                        //Если следующая роль директора
                        if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.DirectorRoleId))
                        {
                            var director = await _userRepository.GetByIdAsync(executors.DirectorId);
                            profile.ConfirmedUserId = director.Id;
                            profile.ConfirmedUserIIN = director.IIN;
                        }
                        //Если следующая роль исполнителя
                        else if(currentStep.ConfirmedRoleId.Equals(ValidationHelpers.ExecutorRoleId))
                        {
                            var executorUser = await _userRepository.GetByIdAsync(executors.ExecutorId);
                            profile.ConfirmedUserId = executorUser.Id;
                            profile.ConfirmedUserIIN = executorUser.IIN;
                        }
                        //Если следующая роль пользователя
                        else if (currentStep.ConfirmedRoleId.Equals(ValidationHelpers.UserRoleId))
                        {
                            var User = await _userRepository.GetByIdAsync(survey.UserId);
                            profile.ConfirmedUserId = User.Id;
                            profile.ConfirmedUserIIN = User.IIN;
                        }
                    }
                    profile.UpdatedAt = DateTime.Now;
                    survey.UpdatedAt = DateTime.Now;
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
                else
                {
                    return returnErrorStatus(false, 403, _localizer["Forbidden"]);
                }
            }
            
        }
        return returnErrorStatus(false, 500, _localizer["UnexpectedError"]);
        }
        catch (Exception ex)
        {
            return returnErrorStatus(false, 500, _localizer["UnexpectedError"]+ex.Message);

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

    private Profile createNewProfile(Survey survey, SendRequestDTO model,Step currentStep,UserDTO currentUser)
    {
        var newProfile = new Profile();
        newProfile.AreaId = survey.BirthAreaId;
        newProfile.StepGroupId = currentStep.StepGroupId;
        newProfile.StepId = currentStep.Id;
        newProfile.RequestedUserId = currentUser.Id;
        newProfile.RequestedUserIIN = currentUser.IIN;
        newProfile.RequestedStatus = model.Status;
        newProfile.RequestedSIGN = model.SignKey;
        newProfile.Status = 0;
        newProfile.ConfirmedStatus = 0;
        newProfile.SurveyId = survey.Id;
        newProfile.CreatedAt = DateTime.Now;
        if (model.Status == -1)
        {
            newProfile.Status = -1;
        }
        
        
        
        return newProfile;

    }
}