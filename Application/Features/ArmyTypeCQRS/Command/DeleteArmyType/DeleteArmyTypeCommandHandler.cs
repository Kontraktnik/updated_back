using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyTypeCQRS.Command.DeleteArmyType;

public class DeleteArmyTypeCommandHandler : IRequestHandler<DeleteArmyTypeCommand,ResponseDTO<bool>>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public DeleteArmyTypeCommandHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<bool>> Handle(DeleteArmyTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedArmyType = await _armyTypeRepository.GetByIdAsync(request.Id);
            if (deletedArmyType != null)
            {
                var armyRank = await _armyTypeRepository.DeleteAsync(deletedArmyType);
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