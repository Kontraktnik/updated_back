using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.RankSalaryCQRS.Command.UpdateRankSalary;

public class UpdateRankSalaryCommandHandler : IRequestHandler<UpdateRankSalaryCommand,ResponseDTO<RankSalaryRDTO>>
{
    
    private readonly IRankSalaryRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateRankSalaryCommandHandler(IRankSalaryRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<RankSalaryRDTO>> Handle(UpdateRankSalaryCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<RankSalary>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<RankSalaryRDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<RankSalaryRDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<RankSalaryRDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<RankSalaryRDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}