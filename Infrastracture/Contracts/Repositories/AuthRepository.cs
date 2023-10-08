using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Application.Contracts.Persistence;
using Application.Contracts.Service;
using Application.DTO.Auth;
using Application.DTO.User;
using Application.Resource;
using AutoMapper;
using Domain.Models.NotificationModels;
using Domain.Models.UserModels;
using Infrastracture.Database;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastracture.Contracts.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IPhoneNotificationRepository _phoneNotification;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> _localizer;
    private IEmailService _emailService;
    public AuthRepository(AppDbContext appDbContext,IPhoneNotificationRepository phoneNotification, IMapper mapper,IStringLocalizer<Localize> localizer,IEmailService emailService)
    {
        _appDbContext = appDbContext;
        _phoneNotification = phoneNotification;
        _mapper = mapper;
        _localizer = localizer;
        _emailService = emailService;
    }

    public async Task<AuthResponse<bool>> RegisterAsync(User user)
    {
        try
        {
            int existedEmail = await _appDbContext.Users.Where(u => (u.Email == user.Email)).CountAsync();
            int existedPhone = await _appDbContext.Users.Where(u => (u.Phone == user.Phone)).CountAsync();;
            int existedIIN = await _appDbContext.Users.Where(u => (u.IIN == user.IIN)).CountAsync();;
            if (existedEmail == 0 && existedPhone == 0 && existedIIN == 0)
            {
                user.RoleId = AppConstant.UserRoleId;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Name = user.Name.Trim();
                user.Surname = user.Surname.Trim();
                user.Verified = false;
                user.Status = true;
                user.FullName =
                    $"{user.Name} {user.Surname} {(user.Patronymic != null ? user.Patronymic : string.Empty)}";
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
                await _phoneNotification.sendUserConfirmationCode(user);
                return new AuthResponse<bool>()
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Success = true,
                    Data = true,
                    Message = _localizer["Registered_Then_Verify"]
                };
            }
            else if(existedEmail > 0)
            {
                return new AuthResponse<bool>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["Email_Existed"]}",
                    Data = false
                };
            }
            else if(existedIIN > 0)
            {
                return new AuthResponse<bool>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["IIN_Existed"]}",
                    Data = false
                };
            }
            else if(existedPhone > 0)
            {
                return new AuthResponse<bool>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["Phone_Existed"]}",
                    Data = false
                };
            }
            else
            {
                return new AuthResponse<bool>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["ExistedError"]}: {_localizer["User"]}",
                    Data = false
                };
            }
        }
        catch (Exception ex)
        {
            return new AuthResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Success = false,
                Message = ex.Message,
                Data = false
            };
        }
    }

    public async Task<AuthResponse<TokenDTO>> LoginAsync(LoginDTO loginDto)
    {
        var user = await _appDbContext.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.IIN == loginDto.IIN);
        if (user != null)
        {
            if (user.Verified == false)
            {
                var verify_answer = await VerifyUserAsync(loginDto.Code, user);
                if (verify_answer != null)
                {
                    return verify_answer;
                }
            }

            if (user.Status == false)
            {
                return new AuthResponse<TokenDTO>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["Nonactive"]}",
                    Data = null
                }; 
            }
            
            if (BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                var token = GenerateToken(user);
                return new AuthResponse<TokenDTO>()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = $"{_localizer["Welcome"]}",
                    Data = token
                };

            }
            else
            {
                return new AuthResponse<TokenDTO>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["Invalid_Credentials"]}",
                    Data = null
                };
            }
        }
        else
        {
            return new AuthResponse<TokenDTO>()
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Success = false,
                Message = $"{_localizer["Invalid_Credentials"]}",
                Data = null
            };
        }
    }

    public async Task<AuthResponse<TokenDTO>> EcpLoginAsync(EcpLoginDTO loginDto)
    {
        var user = await _appDbContext.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.IIN == loginDto.IIN);
        if (user != null)
        {
            if (user.Verified == false)
            {
                return new AuthResponse<TokenDTO>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["Nonactive"]}",
                    Data = null
                }; 
            }

            if (user.Status == false)
            {
                return new AuthResponse<TokenDTO>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = $"{_localizer["Nonactive"]}",
                    Data = null
                }; 
            }
            
            var token = GenerateToken(user);
            return new AuthResponse<TokenDTO>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Message = $"{_localizer["Welcome"]}",
                Data = token
            };
            

        }
        else
        {
            return new AuthResponse<TokenDTO>()
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Success = false,
                Message = $"{_localizer["Invalid_Credentials"]}",
                Data = null
            };
        }
    }

    public async Task<AuthResponse<TokenDTO>> VerifyAsync(VerifyRegistrationDTO verifyRegistrationDto)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.IIN == verifyRegistrationDto.IIN);
        if (user != null)
        {
            if (user.Verified)
            {
                return  new AuthResponse<TokenDTO>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Message =  $"{_localizer["Already_Verified"]}",
                };
            }
            return  await VerifyUserAsync(verifyRegistrationDto.Code, user) ?? new AuthResponse<TokenDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Message = $"{_localizer["Account_Verified"]}"
            };

        }
        else
        {
            return new AuthResponse<TokenDTO>()
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Success = false,
                Message = $"{_localizer["Invalid_Code"]}",
                Data = null
            };
        }
        
        
        
    }


    //Auth Repository Extensions
    private TokenDTO GenerateToken(User user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("IIN", user.IIN),
                new Claim(ClaimTypes.NameIdentifier, user.IIN),
                new Claim("AreaId", user.AreaId.ToString()),
                new Claim(JwtRegisteredClaimNames.PhoneNumber,user.Phone),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.TitleEn),
                        
            }),
            Expires = DateTime.UtcNow.AddMinutes(AppConstant.JWTExpiredMin),
            Issuer = AppConstant.JWTIssuer,
            Audience = AppConstant.JWTAudience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppConstant.JWTSecret)),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return new TokenDTO()
        {
            Expires = AppConstant.JWTExpiredMin * 60,
            jwtToken = stringToken,
            UserDto = _mapper.Map<UserDTO>(user)
        };
    }

    private async Task<AuthResponse<TokenDTO>?> VerifyUserAsync(string Code, User user)
    {
        if (Code == null)
        {
            return new AuthResponse<TokenDTO>()
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Success = false,
                Message = _localizer["Confirm_Account"],
                Data = null
            }; 
        }
        else
        {
            var confCode = await _appDbContext.PhoneNotifications.Where(p =>
                (p.UserId == user.Id) && (p.Status) && 
                (p.Code == Code) &&
                (p.ExpiredAt > DateTime.UtcNow) &&
                (p.Purpose == AppConstant.RegisterCode)
            ).FirstOrDefaultAsync();
            if (confCode != null)
            {
                confCode.Status = false;
                user.Verified = true;
                _appDbContext.PhoneNotifications.Update(confCode);
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();
                return null;
            }
            else
            {
                return new AuthResponse<TokenDTO>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = _localizer["Invalid_Code"],
                    Data = null
                };  
            }
                    
        }
    }


    

    
}