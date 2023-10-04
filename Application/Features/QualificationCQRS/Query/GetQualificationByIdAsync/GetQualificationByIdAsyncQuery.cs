using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.GetQualificationByIdAsync;

public class GetQualificationByIdAsyncQuery : IRequest<ResponseDTO<QualificationDTO>>
{
    public  long Id { get; set; }

    public GetQualificationByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}