using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Query.CountRankSalaryAsync;

public class CountRankSalaryAsyncQueryHandler : IRequestHandler<CountRankSalaryAsyncQuery,int>
{
    private readonly IRankSalaryRepository _repository;
    private readonly IMapper _mapper;

    public CountRankSalaryAsyncQueryHandler(IRankSalaryRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountRankSalaryAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}