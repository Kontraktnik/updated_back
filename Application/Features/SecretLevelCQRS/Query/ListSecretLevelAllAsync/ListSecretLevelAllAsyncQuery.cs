using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.ListSecretLevelAllAsync;

public class ListSecretLevelAllAsyncQuery : IRequest<ResponseDTO<ICollection<SecretLevelDTO>>>
{
    
}