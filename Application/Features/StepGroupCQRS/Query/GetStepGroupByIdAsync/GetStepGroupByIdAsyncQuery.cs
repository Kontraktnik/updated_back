using Application.DTO.Common;
using Application.DTO.Step;
using MediatR;

namespace Application.Features.StepGroupCQRS.Query.GetStepGroupByIdAsync;

public class GetStepGroupByIdAsyncQuery : IRequest<ResponseDTO<StepGroupDTO>>
{
    public long Id;
    
    public GetStepGroupByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}