using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRankCQRS.Command.UpdateArmyRank;

public class UpdateArmyRankCommandHandler : IRequestHandler<UpdateArmyRankCommand,ResponseDTO<ArmyRankDTO>>
{
    private readonly IArmyRankRepository _armyRankRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateArmyRankCommandHandler(IArmyRankRepository armyRankRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyRankRepository = armyRankRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }
    
    public async Task<ResponseDTO<ArmyRankDTO>> Handle(UpdateArmyRankCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _armyRankRepository.GetByIdAsync(request.Id)) != null)
            {
                var updatedArmyRank = _mapper.Map<ArmyRank>(request.model);
                var armyRank = await _armyRankRepository.UpdateAsync(updatedArmyRank);
                return new ResponseDTO<ArmyRankDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = this.localizer["Updated"],
                    Data = _mapper.Map<ArmyRankDTO>(armyRank)
                };  
            }
            return new ResponseDTO<ArmyRankDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyRankDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}