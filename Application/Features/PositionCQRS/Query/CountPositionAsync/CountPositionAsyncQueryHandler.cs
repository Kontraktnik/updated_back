using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.PositionCQRS.Query.CountPositionAsync;

public class CountPositionAsyncQueryHandler : IRequestHandler<CountPositionAsyncQuery,int>
{
    private readonly IPositionRepository _repository;
    private readonly IMapper _mapper;

    public CountPositionAsyncQueryHandler(IPositionRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountPositionAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}