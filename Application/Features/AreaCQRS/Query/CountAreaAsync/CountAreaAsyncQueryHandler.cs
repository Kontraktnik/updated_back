using Application.Contracts.Persistence;
using Application.DTO;
using MediatR;

namespace Application.Features.AreaCQRS.Query.CountAreaAsync;

public class CountAreaAsyncQueryHandler : IRequestHandler<CountAreaAsyncQuery,int>
{
    private readonly IAreaRepository _areaRepository;


    public CountAreaAsyncQueryHandler(IAreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }
    
    
    public async Task<int> Handle(CountAreaAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _areaRepository.CountAsync(request.specification);
    }
}