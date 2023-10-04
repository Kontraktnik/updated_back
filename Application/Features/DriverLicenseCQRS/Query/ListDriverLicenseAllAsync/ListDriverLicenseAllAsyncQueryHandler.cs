using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.ListDriverLicenseAllAsync;

public class ListDriverLicenseAllAsyncQueryHandler : IRequestHandler<ListDriverLicenseAllAsyncQuery,ResponseDTO<ICollection<DriverLicenseDTO>>>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;

    public ListDriverLicenseAllAsyncQueryHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper)
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<DriverLicenseDTO>>> Handle(ListDriverLicenseAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var driverLicenses = await _driverLicenseRepository.ListAllAsync();
        var driverLicensesDTO = _mapper.Map<ICollection<DriverLicenseDTO>>(driverLicenses);
        return new ResponseDTO<ICollection<DriverLicenseDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = driverLicensesDTO,
        };
    }
}