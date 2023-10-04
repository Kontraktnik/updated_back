using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Command.AddCategoryPosition;

public class AddCategoryPositionCommand : IRequest<ResponseDTO<CategoryPositionDTO>>
{
    public CategoryPositionDTO model { get; set; }

    public AddCategoryPositionCommand(CategoryPositionDTO model)
    {
        this.model = model;
    }
}