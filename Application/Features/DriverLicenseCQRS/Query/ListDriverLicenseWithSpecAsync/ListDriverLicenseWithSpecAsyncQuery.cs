using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.ListDriverLicenseWithSpecAsync;

public class ListDriverLicenseWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<DriverLicenseDTO>>>
{
    public ISpecification<DriverLicense> specification { get; set; }

    public ListDriverLicenseWithSpecAsyncQuery(ISpecification<DriverLicense> specification)
    {
        this.specification = specification;
    }
}