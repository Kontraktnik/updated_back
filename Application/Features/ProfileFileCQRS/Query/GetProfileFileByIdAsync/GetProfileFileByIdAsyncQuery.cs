using Application.DTO.Common;
using Application.DTO.Profile;
using MediatR;

namespace Application.Features.ProfileFileCQRS.Query.GetProfileFileByIdAsync;

public class GetProfileFileByIdAsyncQuery : IRequest<ResponseDTO<ProfileFileDTO>>
{
    public GetProfileFileByIdAsyncQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
    
    
    
}