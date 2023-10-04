using Application.DTO.Common;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Command.DeleteSecretLevel;

public class DeleteSecretLevelCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteSecretLevelCommand(long Id)
    {
        this.Id = Id;
    }
}