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

namespace Application.Features.RelativeCQRS.Command.AddRelative;

public class AddRelativeCommandHandler : IRequestHandler<AddRelativeCommand,ResponseDTO<RelativeDTO>>

{
    private readonly IRelativeRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddRelativeCommandHandler(IRelativeRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<RelativeDTO>> Handle(AddRelativeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<Relative>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<RelativeDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<RelativeDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<RelativeDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}