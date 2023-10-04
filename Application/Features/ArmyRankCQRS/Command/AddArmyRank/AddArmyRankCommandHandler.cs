using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRankCQRS.Command.AddArmyRank;

public class AddArmyRankCommandHandler : IRequestHandler<AddArmyRankCommand,ResponseDTO<ArmyRankDTO>>
{
    private readonly IArmyRankRepository _armyRankRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddArmyRankCommandHandler(IArmyRankRepository armyRankRepository,IMapper mapper,
        IStringLocalizer<Localize> localizer
        )
    {
        _armyRankRepository = armyRankRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyRankDTO>> Handle(AddArmyRankCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newArmyRank = _mapper.Map<ArmyRank>(request.model);
            var armyRank = await _armyRankRepository.AddAsync(newArmyRank);
            return new ResponseDTO<ArmyRankDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<ArmyRankDTO>(armyRank)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyRankDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}