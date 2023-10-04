using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.DriverLicenseCQRS.Query.GetDriverLicenseByIdAsync;

public class GetDriverLicenseByIdAsyncQueryHandler : IRequestHandler<GetDriverLicenseByIdAsyncQuery,ResponseDTO<DriverLicenseDTO>>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetDriverLicenseByIdAsyncQueryHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<DriverLicenseDTO>> Handle(GetDriverLicenseByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var driverLicense = await _driverLicenseRepository.GetByIdAsync(request.Id);
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
                Message = localizer["NotFound"]
            };
        }
    }
}