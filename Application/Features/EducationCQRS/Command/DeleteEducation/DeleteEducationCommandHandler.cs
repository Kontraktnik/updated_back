using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.EducationCQRS.Command.DeleteEducation;

public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand,ResponseDTO<bool>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public DeleteEducationCommandHandler(IEducationRepository educationRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedEducation = await _educationRepository.GetByIdAsync(request.Id);
            if (deletedEducation != null)
            {
                var armyRank = await _educationRepository.DeleteAsync(deletedEducation);
                return new ResponseDTO<bool>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.OK,
                    Message = this.localizer["Deleted"],
                    Data = true
                };  
            }
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"],
                Data = false
            }; 
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
                Data = false
            }; 
        }
    }
}