using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.DriverLicenseCQRS.Command.UpdateDriverLicense;

public class UpdateDriverLicenseCommandHandler : IRequestHandler<UpdateDriverLicenseCommand,ResponseDTO<DriverLicenseDTO>>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateDriverLicenseCommandHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper, IStringLocalizer<Localize> localizer
        )
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<DriverLicenseDTO>> Handle(UpdateDriverLicenseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _driverLicenseRepository.GetByIdAsync(request.Id)) != null)
            {
                var updatedDriverLicense = _mapper.Map<DriverLicense>(request.model);
                var driverLicense = await _driverLicenseRepository.UpdateAsync(updatedDriverLicense);
                return new ResponseDTO<DriverLicenseDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<DriverLicenseDTO>(driverLicense)
                };  
            }
            return new ResponseDTO<DriverLicenseDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<DriverLicenseDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}