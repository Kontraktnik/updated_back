using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.DriverLicenseCQRS.Query.ListDriverLicenseAllAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.ListDriverLicenseWithSpecAsync;

public class ListDriverLicenseWithSpecAsyncQueryHandler : IRequestHandler<ListDriverLicenseWithSpecAsyncQuery,ResponseDTO<ICollection<DriverLicenseDTO>>>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;

    public ListDriverLicenseWithSpecAsyncQueryHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper)
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<DriverLicenseDTO>>> Handle(ListDriverLicenseWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var driverLicenses = await _driverLicenseRepository.ListWithSpecAsync(request.specification);
        var driverLicensesDTO = _mapper.Map<ICollection<DriverLicenseDTO>>(driverLicenses);
        return new ResponseDTO<ICollection<DriverLicenseDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = driverLicensesDTO,
        };
    }
}