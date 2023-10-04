using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Step;
using AutoMapper;
using MediatR;

namespace Application.Features.StepCQRS.Query.ListStepAllWithSpecAsync;

public class ListStepAllWithSpecAsyncQueryHandler : IRequestHandler<ListStepAllWithSpecAsyncQuery,ResponseDTO<ICollection<StepDTO>>>
{
    private IMapper _mapper;
    private IStepRepository _repository;

    public ListStepAllWithSpecAsyncQueryHandler(IStepRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<StepDTO>>> Handle(ListStepAllWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        return new ResponseDTO<ICollection<StepDTO>>
        {
            Success = true,
            StatusCode = (int) HttpStatusCode.OK,
            Data = _mapper.Map<ICollection<StepDTO>>(models)
        };


    }
}