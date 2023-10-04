using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.ListServiceYearAllAsync;

public class ListServiceYearAllAsyncQuery : IRequest<ResponseDTO<ICollection<ServiceYearDTO>>>
{
    
}