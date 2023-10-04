using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.AreaCQRS.Query.GetAreaByIdAsync;

public class GetAreaByIdAsyncQuery : IRequest<ResponseDTO<AreaDTO>>
{
    public long Id { get; set; }


    public GetAreaByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}