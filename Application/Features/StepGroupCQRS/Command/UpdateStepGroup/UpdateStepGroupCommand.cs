using Application.DTO.Common;
using Application.DTO.Step;
using Domain.Models.StepModels;
using MediatR;

namespace Application.Features.StepGroupCQRS.Command.UpdateStepGroup;

public class UpdateStepGroupCommand : IRequest<ResponseDTO<StepGroupDTO>>
{
    public StepGroupDTO model { get; set; }
    public long Id { get; set; }

    public UpdateStepGroupCommand(StepGroupDTO model, long Id)
    {
        this.model = model;
        this.Id = Id;
    }

}