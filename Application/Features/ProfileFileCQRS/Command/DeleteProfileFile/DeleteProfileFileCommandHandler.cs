using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.Resource;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileFileCQRS.Command.DeleteProfileFile;

public class DeleteProfileFileCommandHandler : IRequestHandler<DeleteProfileFileCommand,ResponseDTO<bool>>
{
    public DeleteProfileFileCommandHandler(IProfileFileRepository profileFileRepository, ISurveyRepository surveyRepository, IProfileRepository profileRepository, IStringLocalizer<Localize> localizer)
    {
        _profileFileRepository = profileFileRepository;
        _surveyRepository = surveyRepository;
        _profileRepository = profileRepository;
        _localizer = localizer;
    }

    private readonly IProfileFileRepository _profileFileRepository;
    private readonly ISurveyRepository _surveyRepository;
    private readonly IProfileRepository _profileRepository;
    private IStringLocalizer<Localize> _localizer;

    
    public async Task<ResponseDTO<bool>> Handle(DeleteProfileFileCommand request, CancellationToken cancellationToken)
    {
        //Проверяем существует ли данный профиль
        var profileFile = await _profileFileRepository.GetByIdAsync(request.Id);
        if (profileFile.Equals(null))
        {
            return new ResponseDTO<bool>()
            {
                Success = false,
                StatusCode = 400,
                Message = $"{_localizer["NotFound"]}:{_localizer["ProfileFile"]}"
            };
        }
        //Проверяем существует ли данный профиль
        var profile = await _profileRepository.GetByIdAsync(profileFile.ProfileId);
        if (profile.Equals(null))
        {
            return new ResponseDTO<bool>()
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
            return new ResponseDTO<bool>()
            {
                Success = false,
                StatusCode = 400,
                Message = $"{_localizer["Forbidden"]}"
            };
        }
        if (!profileFile.UserId.Equals(request.currentUser.Id))
        {
            return new ResponseDTO<bool>()
            {
                Success = false,
                StatusCode = 400,
                Message = $"{_localizer["Forbidden"]}"
            };
        }

        await _profileFileRepository.DeleteAsync(profileFile);
        return new ResponseDTO<bool>()
        {
            Success = true,
            StatusCode = 200,
            Message = $"{_localizer["Deleted"]}",
            Data = true
        };
    }
}