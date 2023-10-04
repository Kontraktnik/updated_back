using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionByIdAsync;

public class GetCategoryPositionByIdAsyncQuery : IRequest<ResponseDTO<CategoryPositionDTO>>
{
    public  long Id { get; set; }

    public GetCategoryPositionByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}