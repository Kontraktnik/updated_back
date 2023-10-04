using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Step;
using AutoMapper;
using MediatR;

namespace Application.Features.StepGroupCQRS.Query.ListStepGroupWithSpecAsync;

public class ListStepGroupWithSpecAsyncQueryHandler : IRequestHandler<ListStepGroupWithSpecAsyncQuery,ResponseDTO<ICollection<StepGroupDTO>>>
{
    private IStepGroupRepository _repository;
    private IMapper _mapper;

    public ListStepGroupWithSpecAsyncQueryHandler(IStepGroupRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<StepGroupDTO>>> Handle(ListStepGroupWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.ListWithSpecAsync(request.specification);
        
            return new ResponseDTO<ICollection<StepGroupDTO>>()
            {
                StatusCode = 200,
                Success = true,
                Data = _mapper.Map<ICollection<StepGroupDTO>>(model)
            };
        
        
    }
}