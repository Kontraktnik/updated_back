using Application.DTO.Calculation;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Command.UpdateServiceYear;

public class UpdateServiceYearCommand : IRequest<ResponseDTO<ServiceYearDTO>>
{
    public long Id { get; set; }
    public ServiceYearDTO model { get; set; }

    public UpdateServiceYearCommand(ServiceYearDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}