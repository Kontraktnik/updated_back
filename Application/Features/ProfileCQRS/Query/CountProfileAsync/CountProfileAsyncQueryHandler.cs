using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.CountProfileAsync;

public class CountProfileStatusAsyncQueryHandler : IRequestHandler<CountProfileAsyncQuery,int>
{
    private readonly IProfileRepository _repository;
    private readonly IMapper _mapper;

    public CountProfileStatusAsyncQueryHandler(IProfileRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountProfileAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}