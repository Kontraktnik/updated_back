using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.ListQualificationAllAsync;

public class ListQualificationAllAsyncQuery : IRequest<ResponseDTO<ICollection<QualificationDTO>>>
{
    
}