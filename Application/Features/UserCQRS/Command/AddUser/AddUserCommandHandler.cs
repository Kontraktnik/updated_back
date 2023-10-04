using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.User;
using Application.Resource;
using AutoMapper;
using Domain.Models.UserModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.UserCQRS.Command.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand,ResponseDTO<UserDTO>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddUserCommandHandler(IUserRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<UserDTO>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = _mapper.Map<User>(request.model);
            
            if ((await _repository.getUserByEmail(model.Email)) != null)
            {
                return new ResponseDTO<UserDTO>()
                {
                    Success = false,
                    Message = this.localizer["ExistedError"] + ":" + this.localizer["Email"],
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            if ((await _repository.getUserByIIN(model.IIN)) != null)
            {
                return new ResponseDTO<UserDTO>()
                {
                    Success = false,
                    Message =this.localizer["ExistedError"] + ":" + this.localizer["IIN"],
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            if ((await _repository.getUserByPhone(model.Phone)) != null)
            {
                return new ResponseDTO<UserDTO>()
                {
                    Success = false,
                    Message = this.localizer["ExistedError"] + ":" + this.localizer["Phone"],
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            model.FullName = $"{model.Name} {model.Surname} {(model.Patronymic != null ? model.Patronymic : string.Empty)}".Trim();
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var newModel = await _repository.AddAsync(model);
            return new ResponseDTO<UserDTO>()
            {
                Success = true,
                Message = this.localizer["Created"],
                StatusCode = (int)HttpStatusCode.Created,
                Data = _mapper.Map<UserDTO>(newModel)
            };

        }
        catch (Exception ex)
        {
            return new ResponseDTO<UserDTO>()
            {
                Success = false,
                Message = ex.Message.ToString(),
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}