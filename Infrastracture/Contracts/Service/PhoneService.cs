using Application.Contracts.Service;
using Application.DTO.EmailDTO;
using Application.DTO.Survey;
using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Infrastracture.Contracts.Service
{
    public class PhoneService : IPhoneNotification
    {
        private IConfiguration _configuration;
        private string SMSLogin;
        private string SMSPassword;
        private string URL = "https://smsc.kz/sys/send.php";
        public PhoneService(IConfiguration configuration)
        {
            _configuration = configuration;
            AppConfig _config = _configuration
           .GetSection("AppConfig")
           .Get<AppConfig>();
            SMSLogin = _config.SMSLogin??"jauynger";
            SMSPassword = _config.SMSPassword?? "js!qPdTXDHU7KGE";


        }

        public async Task SendMessageForRegistration(string phone,string code)
        {
            try
            {
                string message = String.Format("Для регистрации на портале jauynger.gov.kz введите - {0}, никому не сообщайте код", code);
                await this.SendMessage(phone, message);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task SendNotificationByPhone(SurveyDTO? surveyDto)
        {
            try
            {
                var message = $"Уважаемый {surveyDto.User.FullName ?? "кандидат"}, " +
                             $"статус вашей заявки на портале jauynger.gov.kz изменился.Для проверки текущего статуса зайдите в личный кабинет." +
                             $"Текущий этап - {surveyDto.CurrentStep.TitleRu ?? ""}, статус изменен на - ";
                if (surveyDto.Status == 0)
                {
                    message += " в работе";
                }

                if (surveyDto.Status == 1)
                {
                    message += " принято";
                }
                if (surveyDto.Status == -1)
                {
                    message += " отказано. Отмечаем, что в случае несогласия с ответом государственного органа Вы вправе его обжаловать в соответствии со статьей 89 Административного процедурно-процессуального кодекса. ";
                }
                this.SendMessage(surveyDto.User.Phone, message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }


        private async Task SendMessage(string phone,string message)
        {
            using var client = new HttpClient();

            var uri = new UriBuilder(URL);
            uri.Query = String.Format("login={0}&psw={1}&phones={2}&mes={3}",SMSLogin,SMSPassword,phone,message);
            var url = uri.ToString();
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
