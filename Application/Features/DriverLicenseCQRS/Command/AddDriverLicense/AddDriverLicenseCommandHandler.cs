using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.DriverLicenseCQRS.Command.AddDriverLicense;

public class AddDriverLicenseCommandHandler : IRequestHandler<AddDriverLicenseCommand,ResponseDTO<DriverLicenseDTO>>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddDriverLicenseCommandHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<DriverLicenseDTO>> Handle(AddDriverLicenseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newDriverLicense = _mapper.Map<DriverLicense>(request.model);
            var driverLicense  = await _driverLicenseRepository.AddAsync(newDriverLicense);
            return new ResponseDTO<DriverLicenseDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<DriverLicenseDTO>(driverLicense)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<DriverLicenseDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}