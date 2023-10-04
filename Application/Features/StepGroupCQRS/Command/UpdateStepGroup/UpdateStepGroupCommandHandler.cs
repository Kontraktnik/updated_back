using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Step;
using Application.Resource;
using AutoMapper;
using Domain.Models.StepModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.StepGroupCQRS.Command.UpdateStepGroup;

public class UpdateStepGroupCommandHandler : IRequestHandler<UpdateStepGroupCommand,ResponseDTO<StepGroupDTO>>
{
    private IStepGroupRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public UpdateStepGroupCommandHandler(IStepGroupRepository repository, IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }
    
    
    public async Task<ResponseDTO<StepGroupDTO>> Handle(UpdateStepGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var stepGroup = await _repository.GetByIdAsync(request.Id);
            if (stepGroup != null)
            {
                var newModel = _mapper.Map<StepGroup>(request.model);
                var model = await _repository.UpdateAsync(newModel);
                return new ResponseDTO<StepGroupDTO>()
                {
                    StatusCode = 200,
                    Success = true,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<StepGroupDTO>(model)
                };
            }
            else
            {
                return new ResponseDTO<StepGroupDTO>()
                {
                    StatusCode = 404,
                    Success = false,
                    Message = localizer["NotFound"]
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<StepGroupDTO>()
            {
                StatusCode = 500,
                Success = false,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
        
    }
}