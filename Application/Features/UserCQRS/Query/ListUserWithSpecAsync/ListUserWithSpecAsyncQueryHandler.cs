using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.User;
using AutoMapper;
using MediatR;

namespace Application.Features.UserCQRS.Query.ListUserWithSpecAsync;

public class ListUserWithSpecAsyncQueryHandler : IRequestHandler<ListUserWithSpecAsyncQuery,ResponseDTO<ICollection<UserDTO>>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    
    public ListUserWithSpecAsyncQueryHandler(IUserRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ResponseDTO<ICollection<UserDTO>>> Handle(ListUserWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.ListWithSpecAsync(request.specification);
        
            return new ResponseDTO<ICollection<UserDTO>>()
            {
                Success = true,
                StatusCode = 200,
                Data = _mapper.Map<ICollection<UserDTO>>(model)
            };
        
        
    }
}