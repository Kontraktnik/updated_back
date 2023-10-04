using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.GetDriverLicenseByIdAsync;

public class GetDriverLicenseByIdAsyncQuery : IRequest<ResponseDTO<DriverLicenseDTO>>
{
    public  long Id { get; set; }

    public GetDriverLicenseByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}