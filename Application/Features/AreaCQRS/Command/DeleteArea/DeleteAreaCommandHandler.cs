using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Application.Features.AreaCQRS.Command.DeleteArea;

public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand,ResponseDTO<bool>>
{
    private readonly IAreaRepository _areaRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteAreaCommandHandler> _logger;
    private IStringLocalizer<Localize> localizer;

    public DeleteAreaCommandHandler(IAreaRepository areaRepository,IMapper mapper, ILogger<DeleteAreaCommandHandler> logger,
     IStringLocalizer<Localize> _localizer
        )
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
        _logger = logger;
        localizer = _localizer;
    }


    public async Task<ResponseDTO<bool>> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var area = await _areaRepository.GetByIdAsync(request.Id);
            await _areaRepository.DeleteAsync(area);
            return new ResponseDTO<bool>()
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = localizer["Deleted"],
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}