using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.ListCategoryPositionAllAsync;

public class ListCategoryPositionAllAsyncQuery : IRequest<ResponseDTO<ICollection<CategoryPositionDTO>>>
{
    
}