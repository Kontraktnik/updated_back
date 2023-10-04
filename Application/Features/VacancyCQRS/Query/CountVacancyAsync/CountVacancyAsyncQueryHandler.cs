using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.VacancyCQRS.Query.CountVacancyAsync;

public class CountVacancyAsyncQueryHandler : IRequestHandler<CountVacancyAsyncQuery,int>
{
    private IVacancyRepository _repository;
    private IMapper _mapper;
    public CountVacancyAsyncQueryHandler(IVacancyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountVacancyAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}