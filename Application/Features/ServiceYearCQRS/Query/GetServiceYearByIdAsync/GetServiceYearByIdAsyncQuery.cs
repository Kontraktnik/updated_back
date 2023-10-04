using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.GetServiceYearByIdAsync;

public class GetServiceYearByIdAsyncQuery : IRequest<ResponseDTO<ServiceYearDTO>>
{
    public  long Id { get; set; }

    public GetServiceYearByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}