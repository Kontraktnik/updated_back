using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRankCQRS.Command.DeleteArmyRank;

public class DeleteArmyRankCommandHandler : IRequestHandler<DeleteArmyRankCommand,ResponseDTO<bool>>
{
    private readonly IArmyRankRepository _armyRankRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;


    public DeleteArmyRankCommandHandler(IArmyRankRepository armyRankRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyRankRepository = armyRankRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }
    
    public async Task<ResponseDTO<bool>> Handle(DeleteArmyRankCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedArmyRank = await _armyRankRepository.GetByIdAsync(request.Id);
            if (deletedArmyRank != null)
            {
                var armyRank = await _armyRankRepository.DeleteAsync(deletedArmyRank);
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
                Message = this.localizer["UnexpectedError"] +":" + ex.ToString(),
                Data = false
            }; 
        }
    }

}