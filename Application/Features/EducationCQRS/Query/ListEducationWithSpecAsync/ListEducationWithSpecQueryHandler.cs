using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.EducationCQRS.Query.ListEducationWithSpecAsync;

public class ListEducationWithSpecQueryHandler : IRequestHandler<ListEducationWithSpecQuery,ResponseDTO<ICollection<EducationDTO>>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;

    public ListEducationWithSpecQueryHandler(IEducationRepository educationRepository,IMapper mapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<EducationDTO>>> Handle(ListEducationWithSpecQuery request, CancellationToken cancellationToken)
    {
        var educations = await _educationRepository.ListWithSpecAsync(request.specification);
        var educationsDTO = _mapper.Map<ICollection<EducationDTO>>(educations);
        return new ResponseDTO<ICollection<EducationDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = educationsDTO,
        };
    }
}