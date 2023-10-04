using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Command.AddDriverLicense;

public class AddDriverLicenseCommand : IRequest<ResponseDTO<DriverLicenseDTO>>
{
    public DriverLicenseDTO model { get; set; }

    public AddDriverLicenseCommand(DriverLicenseDTO model)
    {
        this.model = model;
    }
}