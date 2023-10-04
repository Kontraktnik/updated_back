using Application.DTO.Calculation;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Command.UpdateJobCategory;

public class UpdateJobCategoryCommand : IRequest<ResponseDTO<JobCategoryDTO>>
{
    public long Id { get; set; }
    public JobCategoryDTO model { get; set; }

    public UpdateJobCategoryCommand(JobCategoryDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}