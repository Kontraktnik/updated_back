using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Query.ListMedicalStatusAllAsync;

public class ListMedicalStatusAllAsyncQuery : IRequest<ResponseDTO<ICollection<MedicalStatusDTO>>>
{
    
}