using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Application.Features.AreaCQRS.Command.AddArea;

public class AddAreaCommandHandler : IRequestHandler<AddAreaCommand,ResponseDTO<AreaDTO>>
{
    private readonly IAreaRepository _areaRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddAreaCommandHandler> _logger;
    private IStringLocalizer<Localize> localizer;
    public AddAreaCommandHandler(IAreaRepository areaRepository,IMapper mapper, ILogger<AddAreaCommandHandler> logger, IStringLocalizer<Localize> _localizer)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
        _logger = logger;
        localizer = _localizer;

    }
    
    
    public async Task<ResponseDTO<AreaDTO>> Handle(AddAreaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var area = _mapper.Map<Area>(request.model);
            var areaNew = await _areaRepository.AddAsync(area);
            return new ResponseDTO<AreaDTO>()
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = this.localizer["Created"],
                Data = _mapper.Map<AreaDTO>(areaNew)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<AreaDTO>()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
        
    }
}