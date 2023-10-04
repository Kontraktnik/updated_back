using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.EducationCQRS.Command.UpdateEducation;

public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand,ResponseDTO<EducationDTO>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateEducationCommandHandler(IEducationRepository educationRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<EducationDTO>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _educationRepository.GetByIdAsync(request.Id)) != null)
            {
                var updatedEducation = _mapper.Map<Education>(request.model);
                var education = await _educationRepository.UpdateAsync(updatedEducation);
                return new ResponseDTO<EducationDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<EducationDTO>(education)
                };  
            }
            return new ResponseDTO<EducationDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<EducationDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}