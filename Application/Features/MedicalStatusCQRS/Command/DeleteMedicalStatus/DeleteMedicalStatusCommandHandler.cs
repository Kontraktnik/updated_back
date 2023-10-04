using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.MedicalStatusCQRS.Command.DeleteMedicalStatus;

public class DeleteMedicalStatusCommandHandler : IRequestHandler<DeleteMedicalStatusCommand,ResponseDTO<bool>>
{
    private readonly IMedicalStatusRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public DeleteMedicalStatusCommandHandler(IMedicalStatusRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteMedicalStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = await _repository.GetByIdAsync(request.Id);
            if (model != null)
            {
                var armyRank = await _repository.DeleteAsync(model);
                return new ResponseDTO<bool>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.OK,
                    Message = this.localizer["Deleted"],
                    Data = true
                };  
            }
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"],
                Data = false
            }; 
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
                Data = false
            }; 
        }
    }
}