using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Features.JobCategoryCQRS.Command.DeleteJobCategory;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.RankSalaryCQRS.Command.DeleteRankSalary;

public class DeleteRankSalaryCommandHandler : IRequestHandler<DeleteRankSalaryCommand,ResponseDTO<bool>>
{
    private readonly IRankSalaryRepository _repository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public DeleteRankSalaryCommandHandler(IRankSalaryRepository repository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteRankSalaryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedModel = await _repository.GetByIdAsync(request.Id);
            if (deletedModel != null)
            {
                var armyRank = await _repository.DeleteAsync(deletedModel);
                return new ResponseDTO<bool>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.OK,
                    Message = localizer["Deleted"],
                    Data = true
                };  
            }
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
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