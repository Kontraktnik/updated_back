using Application.DTO.Common;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Command.DeleteCategoryPosition;

public class DeleteCategoryPositionCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteCategoryPositionCommand(long Id)
    {
        this.Id = Id;
    }
}