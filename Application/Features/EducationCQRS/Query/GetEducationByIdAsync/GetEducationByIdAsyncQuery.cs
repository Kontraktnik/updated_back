using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.EducationCQRS.Query.GetEducationByIdAsync;

public class GetEducationByIdAsyncQuery : IRequest<ResponseDTO<EducationDTO>>
{
    public  long Id { get; set; }

    public GetEducationByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}