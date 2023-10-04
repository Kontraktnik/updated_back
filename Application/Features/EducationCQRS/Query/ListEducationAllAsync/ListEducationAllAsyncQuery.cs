using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.EducationCQRS.Query.ListEducationAllAsync;

public class ListEducationAllAsyncQuery : IRequest<ResponseDTO<ICollection<EducationDTO>>>
{
    
}