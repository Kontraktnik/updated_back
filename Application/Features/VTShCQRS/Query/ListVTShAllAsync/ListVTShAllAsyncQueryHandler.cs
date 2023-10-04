using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.VTShCQRS.Query.ListVTShAllAsync;

public class ListVTShAllAsyncQueryHandler : IRequestHandler<ListVTShAllAsyncQuery,ResponseDTO<ICollection<VTShDTO>>>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;

    public ListVTShAllAsyncQueryHandler(IVTShRepository vtShRepository,IMapper mapper)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
    }


    public async Task<ResponseDTO<ICollection<VTShDTO>>> Handle(ListVTShAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var vtShAll = await _vtShRepository.ListAllAsync();
        var vtShAllDTO = _mapper.Map<ICollection<VTShDTO>>(vtShAll);
        return new ResponseDTO<ICollection<VTShDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = vtShAllDTO,
        };
    }
}