using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.GetDriverLicenseWithSpecAsync;

public class GetDriverLicenseWithSpecAsyncQuery : IRequest<ResponseDTO<DriverLicenseDTO>>
{
    public ISpecification<DriverLicense> specification { get; set; }

    public GetDriverLicenseWithSpecAsyncQuery(ISpecification<DriverLicense> specification)
    {
        this.specification = specification;
    }
}