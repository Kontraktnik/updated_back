using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.AreaCQRS.Query.ListAreaAllAsync;

public class ListAreaAllAsyncQueryHandler : IRequestHandler<ListAreaAllAsyncQuery,ResponseDTO<ICollection<AreaDTO>>>
{
    private IAreaRepository _areaRepository;
    private IMapper _mapper;

    public ListAreaAllAsyncQueryHandler(IAreaRepository areaRepository,IMapper mapper)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
    }


    public async Task<ResponseDTO<ICollection<AreaDTO>>> Handle(ListAreaAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var areas = await _areaRepository.ListAllAsync();
        return new ResponseDTO<ICollection<AreaDTO>>()
        {
            Success = true,
            StatusCode = (int)HttpStatusCode.OK,
            Data = _mapper.Map<ICollection<AreaDTO>>(areas)
        };
    }
}