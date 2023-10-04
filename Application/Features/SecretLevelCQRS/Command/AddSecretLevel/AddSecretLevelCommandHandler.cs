using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.CalculationModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SecretLevelCQRS.Command.AddSecretLevel;

public class AddSecretLevelCommandHandler : IRequestHandler<AddSecretLevelCommand,ResponseDTO<SecretLevelDTO>>

{
    private readonly ISecretLevelRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public AddSecretLevelCommandHandler(ISecretLevelRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<SecretLevelDTO>> Handle(AddSecretLevelCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newModel = _mapper.Map<SecretLevel>(request.model);
            newModel = await _repository.AddAsync(newModel);
            return new ResponseDTO<SecretLevelDTO>()
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<SecretLevelDTO>(newModel)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<SecretLevelDTO>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}