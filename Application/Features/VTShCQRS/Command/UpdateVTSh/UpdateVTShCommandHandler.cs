using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VTShCQRS.Command.UpdateVTSh;

public class UpdateVTShCommandHandler : IRequestHandler<UpdateVTShCommand,ResponseDTO<VTShDTO>>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public UpdateVTShCommandHandler(IVTShRepository vtShRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<VTShDTO>> Handle(UpdateVTShCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _vtShRepository.GetByIdAsync(request.Id)) != null)
            {
                var updatedVTSh = _mapper.Map<VTSh>(request.model);
                var vTSh = await _vtShRepository.UpdateAsync(updatedVTSh);
                return new ResponseDTO<VTShDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<VTShDTO>(vTSh)
                };  
            }
            return new ResponseDTO<VTShDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
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