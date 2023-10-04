using Application.Contracts.Service;
using Application.DTO.EmailDTO;
using Application.DTO.Survey;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastracture.Contracts.Service;

public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;
    public EmailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    
    
    public async Task SendEmailAsync(Email mailRequest)
    {

        await SendEmail(mailRequest);
    }

    public async Task SendNotificationEmail(SurveyDTO? surveyDto)
    {
        try
        {
            if (surveyDto != null)
            {
                var email = new Email();
                email.To = surveyDto.Email;
                email.Subject = "Изменение статуса заявки №" + surveyDto.Id;
                email.Body = $"Уважаемый {surveyDto.User.FullName ?? "кандидат"}, " +
                             $"статус вашей заявки изменился.Для проверки текущего статуса зайдите в личный кабинет jauynger.gov.kz." +
                             $"Текущий этап - {surveyDto.CurrentStep.TitleRu??""}, статус изменен на - ";
                if (surveyDto.Status == 0)
                {
                    email.Body += " в работе";
                }

                if (surveyDto.Status == 1)
                {
                    email.Body += " принято";
                }
                if (surveyDto.Status == -1)
                {
                    email.Body += " отказано. Отмечаем, что в случае несогласия с ответом государственного органа Вы вправе его обжаловать в соответствии со статьей 89 Административного процедурно-процессуального кодекса. ";
                }
                SendEmail(email);
            }
            

        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    private async Task SendEmail(Email mailRequest)
    {
       /* try
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.To));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        } */
        
    }
}