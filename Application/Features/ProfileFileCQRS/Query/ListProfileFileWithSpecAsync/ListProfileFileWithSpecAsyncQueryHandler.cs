using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileFileCQRS.Query.ListProfileFileWithSpecAsync;

public class ListProfileFileWithSpecAsyncQueryHandler : IRequestHandler<ListProfileFileWithSpecAsyncQuery,ResponseDTO<ICollection<ProfileFileDTO>>>
{
    public ListProfileFileWithSpecAsyncQueryHandler(IProfileFileRepository profileFileRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _profileFileRepository = profileFileRepository;
        _mapper = mapper;
        _localizer = localizer;
    }

    private readonly IProfileFileRepository _profileFileRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> _localizer;
    
    public async Task<ResponseDTO<ICollection<ProfileFileDTO>>> Handle(ListProfileFileWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<ICollection<ProfileFileDTO>>(await _profileFileRepository.ListWithSpecAsync(request._specification));
        return new ResponseDTO<ICollection<ProfileFileDTO>>()
        {
            Success = true,
            StatusCode = (int) HttpStatusCode.OK,
            Data = result
        };
    }
}