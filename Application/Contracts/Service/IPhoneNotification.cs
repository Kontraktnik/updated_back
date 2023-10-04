using Application.DTO.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Service
{
    public interface IPhoneNotification
    {
        Task SendMessageForRegistration(string phone,string code);
        Task SendNotificationByPhone(SurveyDTO? surveyDto);

    }
}
