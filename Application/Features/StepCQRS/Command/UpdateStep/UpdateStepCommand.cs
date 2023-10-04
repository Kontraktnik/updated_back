using Application.DTO.Common;
using Application.DTO.Step;
using MediatR;

namespace Application.Features.StepCQRS.Command.UpdateStep;

public class UpdateStepCommand : IRequest<ResponseDTO<StepDTO>>
{
    public long Id;
    public StepUpdateDTO model;

    public UpdateStepCommand(long id, StepUpdateDTO stepUpdateDto)
    {
        Id = id;
        model = stepUpdateDto;
    }
}