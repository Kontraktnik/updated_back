using Application.DTO.Common;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Command.DeleteDriverLicense;

public class DeleteDriverLicenseCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteDriverLicenseCommand(long Id)
    {
        this.Id = Id;
    }
}