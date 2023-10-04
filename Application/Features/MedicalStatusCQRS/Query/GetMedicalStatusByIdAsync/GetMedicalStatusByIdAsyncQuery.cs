using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Query.GetMedicalStatusByIdAsync;

public class GetMedicalStatusByIdAsyncQuery : IRequest<ResponseDTO<MedicalStatusDTO>>
{
    public  long Id { get; set; }

    public GetMedicalStatusByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}