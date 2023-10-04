using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.DriverLicenseCQRS.Command.DeleteDriverLicense;

public class DeleteDriverLicenseCommandHandler : IRequestHandler<DeleteDriverLicenseCommand,ResponseDTO<bool>>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public DeleteDriverLicenseCommandHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteDriverLicenseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedArmyType = await _driverLicenseRepository.GetByIdAsync(request.Id);
            if (deletedArmyType != null)
            {
                var armyRank = await _driverLicenseRepository.DeleteAsync(deletedArmyType);
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