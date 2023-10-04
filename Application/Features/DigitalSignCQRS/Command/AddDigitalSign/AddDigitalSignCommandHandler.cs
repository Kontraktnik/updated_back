using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using AutoMapper;
using Domain.Models.DigitalSignModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.DigitalSignCQRS.Command.AddDigitalSign;

public class AddDigitalSignCommandHandler : IRequestHandler<AddDigitalSignCommand, ResponseDTO<long?>>
{
    private readonly IDigitalSignRepository _digitalSignRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> Localizer { get; set; }

    public AddDigitalSignCommandHandler(IDigitalSignRepository digitalSignRepository,
        IMapper mapper,
        IStringLocalizer<Localize> localizer
    )
    {
        _digitalSignRepository = digitalSignRepository;
        _mapper = mapper;
        this.Localizer = localizer;
    }


    public async Task<ResponseDTO<long?>> Handle(AddDigitalSignCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var digitalSign = _mapper.Map<DigitalSign>(request.model);

            digitalSign.BinaryData = new()
            {
                Data = request.binaryData
            };

            digitalSign.Info = new()
            {
                Bin = request.digitalSignInfo.Bin,
                FullName = request.digitalSignInfo.FullName,
                Iin = request.digitalSignInfo.Iin,
                Issuer = request.digitalSignInfo.Issuer,
                NotAfter = request.digitalSignInfo.NotAfter,
                NotBefore = request.digitalSignInfo.NotBefore,
                Organization = request.digitalSignInfo.Organization,
                SerialNumber = request.digitalSignInfo.SerialNumber,
                UserType = request.digitalSignInfo.UserType
            };

            var newDigitalSign = await _digitalSignRepository.AddAsync(digitalSign);

            return new ResponseDTO<long?>()
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.Created,
                Message = $"{this.Localizer["Created"]}",
                Data = newDigitalSign.Id
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<long?>()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = ex.ToString(),
                Data = null
            };
        }        
    }
}