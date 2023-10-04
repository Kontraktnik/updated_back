using Application.DTO.EmailDTO;
using Application.DTO.Survey;

namespace Application.Contracts.Service;

public interface IEmailService
{
    Task SendEmailAsync(Email mailRequest);
    Task SendNotificationEmail(SurveyDTO? surveyDto);
}