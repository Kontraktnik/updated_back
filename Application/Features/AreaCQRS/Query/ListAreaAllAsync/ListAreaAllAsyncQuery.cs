using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.AreaCQRS.Query.ListAreaAllAsync;

public class ListAreaAllAsyncQuery : IRequest<ResponseDTO<ICollection<AreaDTO>>>
{
    
}