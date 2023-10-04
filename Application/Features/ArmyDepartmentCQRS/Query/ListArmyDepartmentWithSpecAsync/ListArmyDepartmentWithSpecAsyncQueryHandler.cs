using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Query.ListArmyDepartmentWithSpecAsync;

public class ListArmyDepartmentWithSpecAsyncQueryHandler : IRequestHandler<ListArmyDepartmentWithSpecAsyncQuery,ResponseDTO<ICollection<ArmyDepartmentDTO>>>
{
    private readonly IArmyDepartmentRepository _armyDepartmentRepository;
    private readonly IMapper _mapper;

    public ListArmyDepartmentWithSpecAsyncQueryHandler(IArmyDepartmentRepository armyDepartmentRepository,IMapper mapper)
    {
        _armyDepartmentRepository = armyDepartmentRepository;
        _mapper = mapper;
    }
    public async Task<ResponseDTO<ICollection<ArmyDepartmentDTO>>> Handle(ListArmyDepartmentWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyDepartment = await _armyDepartmentRepository.ListWithSpecAsync(request.specification);
        
        return new ResponseDTO<ICollection<ArmyDepartmentDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = _mapper.Map<ICollection<ArmyDepartmentDTO>>(armyDepartment),
        };
    }
}