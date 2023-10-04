using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.AreaCQRS.Query.ListAreaWithSpecAsync;

public class ListAreaWithSpecAsyncQueryHandler : IRequestHandler<ListAreaWithSpecAsyncQuery,ResponseDTO<ICollection<AreaDTO>>>
{
    private IAreaRepository _areaRepository;
    private IMapper _mapper;


    public ListAreaWithSpecAsyncQueryHandler(IAreaRepository areaRepository,IMapper mapper)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
    }


    public async Task<ResponseDTO<ICollection<AreaDTO>>> Handle(ListAreaWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var areas = await _areaRepository.ListWithSpecAsync(request.specification);
        return new ResponseDTO<ICollection<AreaDTO>>()
        {
            Success = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = _mapper.Map<ICollection<AreaDTO>>(areas)
        };
    }
}