using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Vacancy;
using Application.Resource;
using AutoMapper;
using Domain.Models.VacancyModel;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VacancyCQRS.Command.AddVacancy;

public class AddVacancyCommandHandler : IRequestHandler<AddVacancyCommand,ResponseDTO<VacancyRDTO>>
{
    private IVacancyRepository _repository;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddVacancyCommandHandler(IVacancyRepository repository, IMapper mapper,IUserRepository userRepository,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        this.localizer = localizer;
    }
    
    
    
    public async Task<ResponseDTO<VacancyRDTO>> Handle(AddVacancyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetEntityWithSpecAsync(request.specification);
            if (user != null && user.AreaId != null)
            {
                var model = _mapper.Map<Vacancy>(request.model);
                model.AreaId = user.AreaId ?? 1;
                model.AuthorId = user.Id;
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = DateTime.Now;
                model = await _repository.AddAsync(model);
                return new ResponseDTO<VacancyRDTO>()
                {
                    Success = true,
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = localizer["Created"],
                    Data = _mapper.Map<VacancyRDTO>(model)
                }; 
            }
            else
            {
                return new ResponseDTO<VacancyRDTO>()
                {
                    Success = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = localizer["NotFound"]
                }; 
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<VacancyRDTO>()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
        
        
    }
}