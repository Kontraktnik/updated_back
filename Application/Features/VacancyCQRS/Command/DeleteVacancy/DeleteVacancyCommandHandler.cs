using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Helpers;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VacancyCQRS.Command.DeleteVacancy;

public class DeleteVacancyCommandHandler : IRequestHandler<DeleteVacancyCommand,ResponseDTO<bool>>
{
    private IVacancyRepository _repository;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    
    public DeleteVacancyCommandHandler(IVacancyRepository repository, IMapper mapper,IUserRepository userRepository,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<bool>> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var vacancy = await _repository.GetByIdAsync(request.Id);
            if (vacancy != null)
            {
                //Only AuthorId or AreaId
                if (vacancy.AuthorId == request.UserDto.Id || vacancy.AreaId == request.UserDto.AreaId)
                {
                    await _repository.DeleteAsync(vacancy);
                    return new ResponseDTO<bool>
                    {
                        Success = true,
                        StatusCode = (int) HttpStatusCode.OK,
                        Message = localizer["Deleted"],
                        Data = true
                    }; 
                }
                else
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.Forbidden,
                        Message = localizer["Forbidden"],
                        Data = false
                    }; 
                }
            }
            else
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    StatusCode = (int) HttpStatusCode.NotFound,
                    Message = localizer["NotFound"],
                    Data = false
                }; 
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.ToString(),
                Data = false
            }; 
        }
    }
}