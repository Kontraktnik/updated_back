using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.CountServiceYearAsync;

public class CountServiceYearAsyncQueryHandler : IRequestHandler<CountServiceYearAsyncQuery,int>
{
    private readonly IServiceYearRepository _repository;
    private readonly IMapper _mapper;

    public CountServiceYearAsyncQueryHandler(IServiceYearRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountServiceYearAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}