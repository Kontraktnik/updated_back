using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VTShCQRS.Command.DeleteVTSh;

public class DeleteVTShCommandHandler : IRequestHandler<DeleteVTShCommand,ResponseDTO<bool>>
{
    private readonly IVTShRepository _vtShRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public DeleteVTShCommandHandler(IVTShRepository vtShRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _vtShRepository = vtShRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteVTShCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedVTSh = await _vtShRepository.GetByIdAsync(request.Id);
            if (deletedVTSh != null)
            {
                var armyRank = await _vtShRepository.DeleteAsync(deletedVTSh);
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
                Message = ex.Message,
                Data = false
            }; 
        }
    }
}