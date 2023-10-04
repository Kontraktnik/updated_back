using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.CountSecretLevelAsync;

public class CountSecretLevelAsyncQueryHandler : IRequestHandler<CountSecretLevelAsyncQuery,int>
{
    private readonly ISecretLevelRepository _repository;
    private readonly IMapper _mapper;

    public CountSecretLevelAsyncQueryHandler(ISecretLevelRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountSecretLevelAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}