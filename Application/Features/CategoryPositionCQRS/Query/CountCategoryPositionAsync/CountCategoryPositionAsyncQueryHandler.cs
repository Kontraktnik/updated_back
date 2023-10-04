using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.CountCategoryPositionAsync;

public class CountCategoryPositionAsyncQueryHandler : IRequestHandler<CountCategoryPositionAsyncQuery,int>
{
    private readonly ICategoryPositionRepository _repository;
    private readonly IMapper _mapper;

    public CountCategoryPositionAsyncQueryHandler(ICategoryPositionRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountCategoryPositionAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}