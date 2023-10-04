using Application.DTO.Common;
using Domain.Models.NotificationModels;
using Domain.Models.UserModels;

namespace Application.Contracts.Persistence;

public interface IPhoneNotificationRepository : IGenericRepository<PhoneNotification>
{
    public Task<bool> sendUserConfirmationCode(User user);

    public Task<ResponseDTO<bool>> sendUserConfirmationCodeAgain(string IIN);
}