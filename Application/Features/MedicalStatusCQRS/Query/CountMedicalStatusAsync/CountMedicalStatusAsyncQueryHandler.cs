using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Query.CountMedicalStatusAsync;

public class CountMedicalStatusAsyncQueryHandler : IRequestHandler<CountMedicalStatusAsyncQuery,int>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;

    public CountMedicalStatusAsyncQueryHandler(IMedicalStatusRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountMedicalStatusAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}