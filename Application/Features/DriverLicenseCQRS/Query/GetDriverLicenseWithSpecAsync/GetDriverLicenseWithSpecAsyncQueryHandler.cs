using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.DriverLicenseCQRS.Query.GetDriverLicenseWithSpecAsync;

public class GetDriverLicenseWithSpecAsyncQueryHandler : IRequestHandler<GetDriverLicenseWithSpecAsyncQuery,ResponseDTO<DriverLicenseDTO>>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetDriverLicenseWithSpecAsyncQueryHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<DriverLicenseDTO>> Handle(GetDriverLicenseWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var driverLicense = await _driverLicenseRepository.GetEntityWithSpecAsync(request.specification);
        if (driverLicense != null)
        {
            return new ResponseDTO<DriverLicenseDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<DriverLicenseDTO>(driverLicense),
            };
        }
        else
        {
            return new ResponseDTO<DriverLicenseDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = this.localizer["NotFound"]
            };
        }
    }
}