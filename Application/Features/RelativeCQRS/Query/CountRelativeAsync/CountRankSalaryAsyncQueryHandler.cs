using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.RelativeCQRS.Query.CountRelativeAsync;

public class CountRelativeAsyncQueryHandler : IRequestHandler<CountRelativeAsyncQuery,int>
{
    private readonly IRelativeRepository _repository;
    private readonly IMapper _mapper;

    public CountRelativeAsyncQueryHandler(IRelativeRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountRelativeAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}