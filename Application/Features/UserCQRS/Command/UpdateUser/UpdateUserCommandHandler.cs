using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.User;
using Application.Resource;
using AutoMapper;
using Domain.Models.UserModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.UserCQRS.Command.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,ResponseDTO<UserDTO>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateUserCommandHandler(IUserRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }
    
    
    
    public async Task<ResponseDTO<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<UserDTO>()
                {
                    Success = false,
                    Message = this.localizer["NotFound"],
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };  
            }
            var model = _mapper.Map<User>(request.model);
            var userEmail = await _repository.getUserByEmail(model.Email);
            var userIIN = await _repository.getUserByIIN(model.IIN);
            var userPhone = await _repository.getUserByPhone(model.Phone);
            if (userEmail != null && userEmail.Id != request.Id)
            {
                return new ResponseDTO<UserDTO>()
                {
                    Success = false,
                    Message = this.localizer["Email_Existed"],
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            if (userIIN != null && userIIN.Id != request.Id)
            {
                return new ResponseDTO<UserDTO>()
                {
                    Success = false,
                    Message = this.localizer["IIN_Existed"],
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            if (userPhone != null && userPhone.Id != request.Id)
            {
                return new ResponseDTO<UserDTO>()
                {
                    Success = false,
                    Message = this.localizer["Phone_Existed"],
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            model.FullName = $"{model.Name} {model.Surname} {(model.Patronymic != null ? model.Patronymic : string.Empty)}".Trim();
            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }
            else
            {
                model.Password = user.Password;
            }
            model = await _repository.UpdateAsync(model);
            return new ResponseDTO<UserDTO>()
            {
                Success = true,
                Message = this.localizer["Updated"],
                StatusCode = (int)HttpStatusCode.Created,
                Data = _mapper.Map<UserDTO>(model)
            };

        }
        catch (Exception ex)
        {
            return new ResponseDTO<UserDTO>()
            {
                Success = false,
                Message = ex.ToString(),
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}