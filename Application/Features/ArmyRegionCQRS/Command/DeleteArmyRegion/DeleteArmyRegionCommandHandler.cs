using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRegionCQRS.Command.DeleteArmyRegion;

public class DeleteArmyRegionCommandHandler : IRequestHandler<DeleteArmyRegionCommand,ResponseDTO<bool>>
{
    private readonly IArmyRegionRepository _armyRegionRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public DeleteArmyRegionCommandHandler(IArmyRegionRepository armyRegionRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyRegionRepository = armyRegionRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteArmyRegionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedArmyRegion = await _armyRegionRepository.GetByIdAsync(request.Id);
            if (deletedArmyRegion != null)
            {
                var armyRank = await _armyRegionRepository.DeleteAsync(deletedArmyRegion);
                return new ResponseDTO<bool>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.OK,
                    Message = localizer["Deleted"],
                    Data = true
                };  
            }
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
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