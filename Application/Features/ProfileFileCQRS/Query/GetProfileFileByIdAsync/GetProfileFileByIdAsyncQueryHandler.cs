using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileFileCQRS.Query.GetProfileFileByIdAsync;

public class GetProfileFileByIdAsyncQueryHandler : IRequestHandler<GetProfileFileByIdAsyncQuery,ResponseDTO<ProfileFileDTO>>
{
    public GetProfileFileByIdAsyncQueryHandler(IProfileFileRepository profileFileRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _profileFileRepository = profileFileRepository;
        _mapper = mapper;
        _localizer = localizer;
    }

    private readonly IProfileFileRepository _profileFileRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> _localizer;

    public async Task<ResponseDTO<ProfileFileDTO>> Handle(GetProfileFileByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var data = await _profileFileRepository.GetByIdAsync(request.Id);
        if (data != null)
        {
            return new ResponseDTO<ProfileFileDTO>()
            {
                Success = true,
                StatusCode = 200,
                Data = _mapper.Map<ProfileFileDTO>(data)
            };
        }
        return new ResponseDTO<ProfileFileDTO>()
        {
            Success = false,
            StatusCode = 404,
            Message = _localizer["NotFound"]
        };
        
        
    }
}