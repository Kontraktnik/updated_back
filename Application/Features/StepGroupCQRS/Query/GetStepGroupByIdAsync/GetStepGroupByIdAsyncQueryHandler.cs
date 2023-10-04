using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Step;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.StepGroupCQRS.Query.GetStepGroupByIdAsync;

public class GetStepGroupByIdAsyncQueryHandler : IRequestHandler<GetStepGroupByIdAsyncQuery,ResponseDTO<StepGroupDTO>>
{
    private IStepGroupRepository _repository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetStepGroupByIdAsyncQueryHandler(IStepGroupRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<StepGroupDTO>> Handle(GetStepGroupByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(request.Id);
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