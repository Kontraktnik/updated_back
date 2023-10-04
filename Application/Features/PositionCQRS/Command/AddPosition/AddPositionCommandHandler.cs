using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.Features.RankSalaryCQRS.Command.AddRankSalary;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.PositionCQRS.Command.AddPosition;

public class AddPositionCommandHandler : IRequestHandler<AddPositionCommand,ResponseDTO<PositionRDTO>>

{
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddPositionCommandHandler(IPositionRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<PositionRDTO>> Handle(AddPositionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<Position>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<PositionRDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<PositionRDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<PositionRDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}