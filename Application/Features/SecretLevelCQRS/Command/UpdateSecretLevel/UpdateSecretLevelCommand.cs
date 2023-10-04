using Application.DTO.Calculation;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Command.UpdateSecretLevel;

public class UpdateSecretLevelCommand : IRequest<ResponseDTO<SecretLevelDTO>>
{
    public long Id { get; set; }
    public SecretLevelDTO model { get; set; }

    public UpdateSecretLevelCommand(SecretLevelDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}