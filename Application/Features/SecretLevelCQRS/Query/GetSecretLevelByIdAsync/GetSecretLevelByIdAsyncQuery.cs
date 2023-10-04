using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.GetSecretLevelByIdAsync;

public class GetSecretLevelByIdAsyncQuery : IRequest<ResponseDTO<SecretLevelDTO>>
{
    public  long Id { get; set; }

    public GetSecretLevelByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}