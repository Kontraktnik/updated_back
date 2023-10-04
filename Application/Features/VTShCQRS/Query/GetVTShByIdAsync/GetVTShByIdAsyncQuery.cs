using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.VTShCQRS.Query.GetVTShByIdAsync;

public class GetVTShByIdAsyncQuery : IRequest<ResponseDTO<VTShDTO>>
{
    public  long Id { get; set; }

    public GetVTShByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}