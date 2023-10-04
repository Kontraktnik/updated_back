using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Command.UpdateDriverLicense;

public class UpdateDriverLicenseCommand : IRequest<ResponseDTO<DriverLicenseDTO>>
{
    public long Id { get; set; }
    public DriverLicenseDTO model { get; set; }

    public UpdateDriverLicenseCommand(DriverLicenseDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}