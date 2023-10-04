using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Query.CountJobCategoryAsync;

public class CountJobCategoryAsyncQueryHandler : IRequestHandler<CountJobCategoryAsyncQuery,int>
{
    private readonly IJobCategoryRepository _repository;
    private readonly IMapper _mapper;

    public CountJobCategoryAsyncQueryHandler(IJobCategoryRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountJobCategoryAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}