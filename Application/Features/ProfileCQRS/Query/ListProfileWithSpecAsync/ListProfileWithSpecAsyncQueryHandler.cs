using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.System;
using Application.Features.MedicalStatusCQRS.Query.ListMedicalStatusWithSpecAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.ListProfileWithSpecAsync;

public class ListProfileWithSpecAsyncQueryHandler : IRequestHandler<ListProfileWithSpecAsyncQuery,ResponseDTO<ICollection<ProfileDTO>>>
{
    private readonly IProfileRepository _repository;
    private readonly IMapper _mapper;

    public ListProfileWithSpecAsyncQueryHandler(IProfileRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<ProfileDTO>>> Handle(ListProfileWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        var modelsDTO = _mapper.Map<ICollection<ProfileDTO>>(models);
        return new ResponseDTO<ICollection<ProfileDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}