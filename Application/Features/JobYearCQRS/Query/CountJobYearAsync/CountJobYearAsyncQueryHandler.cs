using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.JobYearCQRS.Query.CountJobYearAsync;

public class CountJobYearAsyncQueryHandler : IRequestHandler<CountJobYearAsyncQuery,int>
{
    private readonly IJobYearRepository _repository;
    private readonly IMapper _mapper;

    public CountJobYearAsyncQueryHandler(IJobYearRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountJobYearAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}