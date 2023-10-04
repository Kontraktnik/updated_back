using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Command.AddServiceYear;

public class AddServiceYearCommand : IRequest<ResponseDTO<ServiceYearDTO>>
{
    public ServiceYearDTO model { get; set; }

    public AddServiceYearCommand(ServiceYearDTO model)
    {
        this.model = model;
    }
}