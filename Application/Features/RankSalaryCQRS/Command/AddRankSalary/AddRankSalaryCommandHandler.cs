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

namespace Application.Features.RankSalaryCQRS.Command.AddRankSalary;

public class AddRankSalaryCommandHandler : IRequestHandler<AddRankSalaryCommand,ResponseDTO<RankSalaryRDTO>>

{
    private readonly IRankSalaryRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddRankSalaryCommandHandler(IRankSalaryRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<RankSalaryRDTO>> Handle(AddRankSalaryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<RankSalary>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<RankSalaryRDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<RankSalaryRDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<RankSalaryRDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}