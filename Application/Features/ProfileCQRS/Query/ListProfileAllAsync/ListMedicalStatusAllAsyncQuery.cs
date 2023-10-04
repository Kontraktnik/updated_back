using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.ListProfileAllAsync;

public class ListProfileAllAsyncQuery : IRequest<ResponseDTO<ICollection<ProfileDTO>>>
{
    
}