using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Command.AddJobCategory;

public class AddJobCategoryCommand : IRequest<ResponseDTO<JobCategoryDTO>>
{
    public JobCategoryDTO model { get; set; }

    public AddJobCategoryCommand(JobCategoryDTO model)
    {
        this.model = model;
    }
}