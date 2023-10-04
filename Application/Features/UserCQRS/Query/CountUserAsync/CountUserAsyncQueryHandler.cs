using Application.Contracts.Persistence;
using Application.DTO.Common;
using AutoMapper;
using MediatR;

namespace Application.Features.UserCQRS.Query.CountUserAsync;

public class CountUserAsyncQueryHandler : IRequestHandler<CountUserAsyncQuery,int>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public CountUserAsyncQueryHandler(IUserRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountUserAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountAsync(request.specification);
    }
}