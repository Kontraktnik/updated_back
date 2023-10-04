using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Command.AddSecretLevel;

public class AddSecretLevelCommand : IRequest<ResponseDTO<SecretLevelDTO>>
{
    public SecretLevelDTO model { get; set; }

    public AddSecretLevelCommand(SecretLevelDTO model)
    {
        this.model = model;
    }
}