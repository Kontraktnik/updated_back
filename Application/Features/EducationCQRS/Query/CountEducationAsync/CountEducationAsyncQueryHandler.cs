using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.EducationCQRS.Query.CountEducationAsync;

public class CountEducationAsyncQueryHandler : IRequestHandler<CountEducationAsyncQuery,int>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;

    public CountEducationAsyncQueryHandler(IEducationRepository educationRepository,IMapper mapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountEducationAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _educationRepository.CountAsync(request.specification);
    }
}