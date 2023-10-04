using Application.DTO.Common;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Command.DeleteJobCategory;

public class DeleteJobCategoryCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteJobCategoryCommand(long Id)
    {
        this.Id = Id;
    }
}