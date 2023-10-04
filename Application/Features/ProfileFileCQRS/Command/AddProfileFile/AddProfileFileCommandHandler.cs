using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.User;
using Application.Helpers;
using Application.Resource;
using AutoMapper;
using Domain;
using Domain.Models.ProfileModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileFileCQRS.Command;

public class AddProfileFileCommandHandler : IRequestHandler<AddProfileFileCommand, ResponseDTO<ProfileFileDTO>>
{
    private readonly IProfileFileRepository _profileFileRepository;
    private readonly IProfileRepository _profileRepository;
    private IStringLocalizer<Localize> _localizer;
    private IMapper _mapper;
    private readonly ISurveyRepository _surveyRepository;
    private readonly ISurveyExecutorRepository _surveyExecutorRepository;
    private readonly IStepRepository _stepRepository;
    private readonly AppConfig _config;

    public AddProfileFileCommandHandler(
        IProfileFileRepository profileFileRepository,
        IProfileRepository profileRepository,
        IStringLocalizer<Localize> localizer,
        IMapper mapper,
        ISurveyRepository surveyRepository,
        ISurveyExecutorRepository surveyExecutorRepository,
        IStepRepository stepRepository,
            AppConfig config
        )
    {
        _profileFileRepository = profileFileRepository;
        _profileRepository = profileRepository;
        _localizer = localizer;
        _mapper = mapper;
        _surveyRepository = surveyRepository;
        _surveyExecutorRepository = surveyExecutorRepository;
        _stepRepository = stepRepository;
        _config = config;
    }


    public async Task<ResponseDTO<ProfileFileDTO>> Handle(AddProfileFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = request.model;
            var currentUser = request.UserDto;
            //Проверяем существует ли данный профиль
            var profile = await _profileRepository.GetByIdAsync(model.ProfileId);
            if (profile == null)
            {
                return new ResponseDTO<ProfileFileDTO>()
                {
                    Success = false,
                    StatusCode = 400,
                    Message = $"{_localizer["NotFound"]}:{_localizer["ProfileId"]}"
                };
            }
            //Проверяем активна ли данная заявка
            var survey = await _surveyRepository.GetByIdAsync(profile.SurveyId);
            if (survey.CurrentStepId != profile.StepId)
            {
                return new ResponseDTO<ProfileFileDTO>()
                {
                    Success = false,
                    StatusCode = 400,
                    Message = $"{_localizer["Forbidden"]}"
                };
            }
            //Проверяем уровень доступа пользователя
            var surveyExecutor = await _surveyExecutorRepository.GetBySurveyId(profile.SurveyId);
            var currentStep = await _stepRepository.GetByIdAsync(profile.StepId);
            //Если директор
            if (currentUser.RoleId.Equals(ValidationHelpers.DirectorRoleId))
            {
                //Проверяем имеет ли право директор на данную запись
                //Имеет ли доступ на данном этапе
                if (surveyExecutor.DirectorId != currentUser.Id && (!currentUser.RoleId.Equals(currentStep.RequestedRoleId) || !currentUser.RoleId.Equals(currentStep.ConfirmedRoleId)))
                {
                    return new ResponseDTO<ProfileFileDTO>()
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = $"{_localizer["Forbidden"]}"
                    };
                }
            }
            //Если исполнитель
            if (currentUser.RoleId.Equals(ValidationHelpers.ExecutorRoleId))
            {
                //Проверяем имеет ли право исполнитель на данную запись
                //Имеет ли доступ на данном этапе
                if (surveyExecutor.ExecutorId != currentUser.Id && (!currentUser.RoleId.Equals(currentStep.RequestedRoleId) || !currentUser.RoleId.Equals(currentStep.ConfirmedRoleId)))
                {
                    return new ResponseDTO<ProfileFileDTO>()
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = $"{_localizer["Forbidden"]}"
                    };
                }
            }
            //Другие роли
            if (currentUser.RoleId.Equals(ValidationHelpers.KNBRoleId) || currentUser.RoleId.Equals(ValidationHelpers.MEDRoleId))
            {
                //Проверяем имеет ли право исполнитель на данную запись
                //Имеет ли доступ на данном этапе
                if (survey.BirthAreaId != currentUser.AreaId && (!currentUser.RoleId.Equals(currentStep.RequestedRoleId) || !currentUser.RoleId.Equals(currentStep.ConfirmedRoleId)))
                {
                    return new ResponseDTO<ProfileFileDTO>()
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = $"{_localizer["Forbidden"]}"
                    };
                }
            }
            //Сам пользователь
            if (currentUser.RoleId.Equals(ValidationHelpers.UserRoleId))
            {
                //Проверяем имеет ли право исполнитель на данную запись
                //Имеет ли доступ на данном этапе
                if (survey.UserId != currentUser.Id && (!currentUser.RoleId.Equals(currentStep.RequestedRoleId) || !currentUser.RoleId.Equals(currentStep.ConfirmedRoleId)))
                {
                    return new ResponseDTO<ProfileFileDTO>()
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = $"{_localizer["Forbidden"]}"
                    };
                }
            }
            //Формируем наименование файла
            var path = $"{_config.UploadRequestStoragePath}/{survey.IIN}";
            var filePath = Path.Combine(path, $"{request.FileName}_{Guid.NewGuid()}{request.ExtensionFile}");
            model.File = filePath;
            model.UserId = currentUser.Id;
            var newProfileFile = _mapper.Map<ProfileFile>(model);
            await _profileFileRepository.AddAsync(newProfileFile);
            return new ResponseDTO<ProfileFileDTO>()
            {
                Success = true,
                StatusCode = 200,
                Message = $"{_localizer["Created"]}",
                Data = _mapper.Map<ProfileFileDTO>(newProfileFile)
            };

        }
        catch (Exception ex)
        {
            return new ResponseDTO<ProfileFileDTO>()
            {
                Success = false,
                StatusCode = 500,
                Message = ex.ToString()
            };
        }


    }
}