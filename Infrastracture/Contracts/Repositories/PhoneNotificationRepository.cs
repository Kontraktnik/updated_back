using System.Net;
using Application.Contracts.Persistence;
using Application.Contracts.Service;
using Application.DTO.Common;
using Application.DTO.EmailDTO;
using Application.Resource;
using Domain.Models.NotificationModels;
using Domain.Models.UserModels;
using Infrastracture.Contracts.Service;
using Infrastracture.Database;
using Infrastracture.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Infrastracture.Contracts.Repositories;

public class PhoneNotificationRepository : GenericRepository<PhoneNotification>,IPhoneNotificationRepository
{
    private readonly AppDbContext _appDbContext;
    private IStringLocalizer<Localize> _localizer;
    private IEmailService _emailService;
    private IPhoneNotification _phoneNotification;
    public PhoneNotificationRepository(
        AppDbContext context,
        IStringLocalizer<Localize> localizer,
        IEmailService emailService,
        IPhoneNotification phoneNotification
        
        ) : base(context)
    {
        _appDbContext = context;
        _localizer = localizer;
        _emailService = emailService;
        _phoneNotification = phoneNotification;
    }


    public async Task<bool> sendUserConfirmationCode(User user)
    {
        try
        {
            
            var notification = new PhoneNotification
            {
                Code = NotificationHelper.GeneratePhoneCode(),
                UserId = user.Id,
                Phone = user.Phone,
                Status = true,
                Purpose = AppConstant.RegisterCode,
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1),
            };
            await _appDbContext.PhoneNotifications.AddAsync(notification);
            await _appDbContext.SaveChangesAsync();
            Email email = new Email()
            {
                To = user.Email,
                Subject = "Подтвердите действие \n Verify Your Action \n Әрекетті растау",
                Body = "Ваш код для подтверждения аккаунта: " + notification.Code
            };
            await _emailService.SendEmailAsync(email);
            await _phoneNotification.SendMessageForRegistration(user.Phone,notification.Code);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<ResponseDTO<bool>> sendUserConfirmationCodeAgain(string IIN)
    {
        try
        {
            var user = await _appDbContext.Users.AsNoTracking().Where(u => u.IIN == IIN).FirstOrDefaultAsync();
            if (user != null)
            {
                if (!user.Verified)
                {
                    var expiredNotifications = await _appDbContext.PhoneNotifications.
                        Where(u=>(u.UserId == user.Id) && (u.Status) && (u.Purpose == AppConstant.RegisterCode)).ToListAsync();
                    if (expiredNotifications.Count > 0)
                    {
                        expiredNotifications.ForEach(ph=>ph.Status = false);
                    }

                    await sendUserConfirmationCode(user);
                    return new ResponseDTO<bool>()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Success = true,
                        Data = true,
                        Message = _localizer["Notification"]
                    };  
                }
                else
                {
                    return new ResponseDTO<bool>()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Success = false,
                        Data = false,
                        Message = _localizer["Verified"]
                    };  
                }
            }
            else
            {
                return new ResponseDTO<bool>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Data = false,
                    Message = _localizer["NotFound"]
                }; 
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Success = false,
                Data = false,
                Message = ex.Message
            }; 
        }
    }
}