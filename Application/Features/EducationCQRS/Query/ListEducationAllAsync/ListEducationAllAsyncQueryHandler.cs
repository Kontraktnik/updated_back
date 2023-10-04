using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.EducationCQRS.Query.ListEducationAllAsync;

public class ListEducationAllAsyncQueryHandler : IRequestHandler<ListEducationAllAsyncQuery,ResponseDTO<ICollection<EducationDTO>>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;

    public ListEducationAllAsyncQueryHandler(IEducationRepository educationRepository,IMapper mapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<EducationDTO>>> Handle(ListEducationAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var educations = await _educationRepository.ListAllAsync();
        var educationsDTO = _mapper.Map<ICollection<EducationDTO>>(educations);
        return new ResponseDTO<ICollection<EducationDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = educationsDTO,
        };
    }
}