using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.ListDriverLicenseAllAsync;

public class ListDriverLicenseAllAsyncQuery : IRequest<ResponseDTO<ICollection<DriverLicenseDTO>>>
{
    
}