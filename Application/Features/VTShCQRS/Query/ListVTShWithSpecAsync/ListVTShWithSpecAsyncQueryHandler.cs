using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.VTShCQRS.Query.ListVTShWithSpecAsync;

public class ListVTShWithSpecAsyncQueryHandler : IRequestHandler<ListVTShWithSpecAsyncQuery,ResponseDTO<ICollection<VTShDTO>>>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;

    public ListVTShWithSpecAsyncQueryHandler(IVTShRepository vtShRepository,IMapper mapper)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<VTShDTO>>> Handle(ListVTShWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var vtShAll = await _vtShRepository.ListWithSpecAsync(request.specification);
        var vtShAllDTO = _mapper.Map<ICollection<VTShDTO>>(vtShAll);
        return new ResponseDTO<ICollection<VTShDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = vtShAllDTO,
        };
    }
}