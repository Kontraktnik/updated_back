using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.CountQualificationAsync;

public class CountQualificationAsyncQueryHandler : IRequestHandler<CountQualificationAsyncQuery,int>
{
    private readonly IQualificationRepository _repository;
    private readonly IMapper _mapper;

    public CountQualificationAsyncQueryHandler(IQualificationRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountQualificationAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}