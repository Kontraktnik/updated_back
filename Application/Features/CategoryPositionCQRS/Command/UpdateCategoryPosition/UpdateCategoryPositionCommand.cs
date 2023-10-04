using Application.DTO.Calculation;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Command.UpdateCategoryPosition;

public class UpdateCategoryPositionCommand : IRequest<ResponseDTO<CategoryPositionDTO>>
{
    public long Id { get; set; }
    public CategoryPositionDTO model { get; set; }

    public UpdateCategoryPositionCommand(CategoryPositionDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}