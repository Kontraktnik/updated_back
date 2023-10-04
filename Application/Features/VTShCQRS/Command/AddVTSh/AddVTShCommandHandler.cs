using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VTShCQRS.Command.AddVTSh;

public class AddVTShCommandHandler : IRequestHandler<AddVTShCommand,ResponseDTO<VTShDTO>>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddVTShCommandHandler(IVTShRepository vtShRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<VTShDTO>> Handle(AddVTShCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newVTSh = _mapper.Map<VTSh>(request.model);
            var vTSH  = await _vtShRepository.AddAsync(newVTSh);
            return new ResponseDTO<VTShDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = this.localizer["NotFound"],
                Data = _mapper.Map<VTShDTO>(vTSH)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<VTShDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.Message,
            };
        }
    }
}