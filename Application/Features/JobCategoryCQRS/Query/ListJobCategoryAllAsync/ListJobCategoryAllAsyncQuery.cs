using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Query.ListJobCategoryAllAsync;

public class ListJobCategoryAllAsyncQuery : IRequest<ResponseDTO<ICollection<JobCategoryDTO>>>
{
    
}