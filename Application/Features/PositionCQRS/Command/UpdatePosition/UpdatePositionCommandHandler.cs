using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.PositionCQRS.Command.UpdatePosition;

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand,ResponseDTO<PositionRDTO>>
{
    
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdatePositionCommandHandler(IPositionRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<PositionRDTO>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<Position>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<PositionRDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = this.localizer["Updated"],
                    Data = _mapper.Map<PositionRDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<PositionRDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"]
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<PositionRDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}