using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SecretLevelCQRS.Command.UpdateSecretLevel;

public class UpdateSecretLevelCommandHandler : IRequestHandler<UpdateSecretLevelCommand,ResponseDTO<SecretLevelDTO>>
{
    
    private readonly ISecretLevelRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public UpdateSecretLevelCommandHandler(ISecretLevelRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<SecretLevelDTO>> Handle(UpdateSecretLevelCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            if ((await _repository.GetByIdAsync(request.Id)) != null)
            {
                var Model= _mapper.Map<SecretLevel>(request.model);
                var updatedModel  = await _repository.UpdateAsync(Model);
                return new ResponseDTO<SecretLevelDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message =localizer["Updated"],
                    Data = _mapper.Map<SecretLevelDTO>(updatedModel)
                };  
            }
            return new ResponseDTO<SecretLevelDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<SecretLevelDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}