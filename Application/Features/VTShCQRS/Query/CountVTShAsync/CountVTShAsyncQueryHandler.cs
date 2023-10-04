using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.VTShCQRS.Query.CountVTShAsync;

public class CountVTShAsyncQueryHandler : IRequestHandler<CountVTShAsyncQuery,int>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;

    public CountVTShAsyncQueryHandler(IVTShRepository vtShRepository,IMapper mapper)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
    }


    public async Task<int> Handle(CountVTShAsyncQuery request, CancellationToken cancellationToken)
    { 
        return await _vtShRepository.CountAsync(request.specification);
    }
}