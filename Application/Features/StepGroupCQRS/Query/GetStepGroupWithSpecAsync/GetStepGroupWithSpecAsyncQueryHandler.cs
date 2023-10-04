using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Step;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.StepGroupCQRS.Query.GetStepGroupWithSpecAsync;

public class GetStepGroupWithSpecAsyncQueryHandler : IRequestHandler<GetStepGroupWithSpecAsyncQuery,ResponseDTO<StepGroupDTO>>
{
    private IStepGroupRepository _repository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetStepGroupWithSpecAsyncQueryHandler(IStepGroupRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<StepGroupDTO>> Handle(GetStepGroupWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<StepGroupDTO>()
            {
                StatusCode = 200,
                Success = true,
                Data = _mapper.Map<StepGroupDTO>(model)
            };
        }
        else
        {
            return new ResponseDTO<StepGroupDTO>()
            {
                StatusCode = 404,
                Success = false,
                Message = localizer["NotFound"],
            };
        }
    }
}