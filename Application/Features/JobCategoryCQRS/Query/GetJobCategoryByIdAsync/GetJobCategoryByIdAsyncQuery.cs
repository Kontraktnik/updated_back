using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Query.GetJobCategoryByIdAsync;

public class GetJobCategoryByIdAsyncQuery : IRequest<ResponseDTO<JobCategoryDTO>>
{
    public  long Id { get; set; }

    public GetJobCategoryByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}