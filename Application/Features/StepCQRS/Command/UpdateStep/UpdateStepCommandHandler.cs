using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Step;
using Application.Resource;
using AutoMapper;
using Domain.Models.StepModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.StepCQRS.Command.UpdateStep;

public class UpdateStepCommandHandler : IRequestHandler<UpdateStepCommand,ResponseDTO<StepDTO>>
{
    private IMapper _mapper;
    private IStepRepository _repository;
    private IStringLocalizer<Localize> localizer;
    public UpdateStepCommandHandler(IStepRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<StepDTO>> Handle(UpdateStepCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = await _repository.GetByIdAsync(request.Id);
            if (model != null)
            {
                model = _mapper.Map<Step>(request.model);

                model = await _repository.UpdateAsync(model);
                
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
        catch (Exception exception)
        {
            return new ResponseDTO<StepDTO>
            {
                Success = false,
                StatusCode = 500,
                Message = exception.Message
            };
        }
    }
}