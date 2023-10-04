using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.AreaCQRS.Command.AddArea;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Application.Features.AreaCQRS.Command.UpdateArea;

public class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand,ResponseDTO<AreaDTO>>
{
    private readonly IAreaRepository _areaRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAreaCommand> _logger;
    private IStringLocalizer<Localize> localizer;

    public UpdateAreaCommandHandler(IAreaRepository areaRepository,IMapper mapper, ILogger<UpdateAreaCommand> logger,
        IStringLocalizer<Localize> _localizer
    )
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
        _logger = logger;
        localizer = _localizer;

    }

    public async Task<ResponseDTO<AreaDTO>> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var area = _mapper.Map<Area>(request.model);
            await _areaRepository.UpdateAsync(area);
            return new ResponseDTO<AreaDTO>()
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = localizer["Updated"],
                Data = _mapper.Map<AreaDTO>(area)
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