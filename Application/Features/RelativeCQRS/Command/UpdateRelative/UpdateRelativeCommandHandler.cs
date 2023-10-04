using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.RelativeCQRS.Command.UpdateRelative;

public class UpdateRelativeCommandHandler : IRequestHandler<UpdateRelativeCommand,ResponseDTO<RelativeDTO>>
{
    
    private readonly IRelativeRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public UpdateRelativeCommandHandler(IRelativeRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<RelativeDTO>> Handle(UpdateRelativeCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<Relative>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<RelativeDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = this.localizer["Updated"],
                    Data = _mapper.Map<RelativeDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<RelativeDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<RelativeDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.Message,
            };
        }
    }
}