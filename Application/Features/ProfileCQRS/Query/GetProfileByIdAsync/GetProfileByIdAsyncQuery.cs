using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.GetProfileByIdAsync;

public class GetProfileByIdAsyncQuery : IRequest<ResponseDTO<ProfileDTO>>
{
    public  long Id { get; set; }

    public GetProfileByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}