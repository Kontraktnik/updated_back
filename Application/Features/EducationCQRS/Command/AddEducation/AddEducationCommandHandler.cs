using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.EducationCQRS.Command.AddEducation;

public class AddEducationCommandHandler : IRequestHandler<AddEducationCommand,ResponseDTO<EducationDTO>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddEducationCommandHandler(IEducationRepository educationRepository,IMapper mapper,IStringLocalizer<Localize> localizer
        )
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<EducationDTO>> Handle(AddEducationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newEducation = _mapper.Map<Education>(request.model);
            var education  = await _educationRepository.AddAsync(newEducation);
            return new ResponseDTO<EducationDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = this.localizer["Created"],
                Data = _mapper.Map<EducationDTO>(education)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<EducationDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}