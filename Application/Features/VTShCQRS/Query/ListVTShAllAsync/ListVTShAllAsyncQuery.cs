using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.VTShCQRS.Query.ListVTShAllAsync;

public class ListVTShAllAsyncQuery : IRequest<ResponseDTO<ICollection<VTShDTO>>>
{
    
}