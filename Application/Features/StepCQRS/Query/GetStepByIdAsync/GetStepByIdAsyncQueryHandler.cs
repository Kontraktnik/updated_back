using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Step;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.StepCQRS.Query.GetStepByIdAsync;

public class GetStepByIdAsyncQueryHandler : IRequestHandler<GetStepByIdAsyncQuery,ResponseDTO<StepDTO>>
{
    private IMapper _mapper;
    private IStepRepository _repository;
    private IStringLocalizer<Localize> localizer;
    public GetStepByIdAsyncQueryHandler(IStepRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<StepDTO>> Handle(GetStepByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<StepDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.OK,
                Data = _mapper.Map<StepDTO>(model)
            };
        }
        else
        {
            return new ResponseDTO<StepDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"]
            };
        }
        
        
        
        
    }
}