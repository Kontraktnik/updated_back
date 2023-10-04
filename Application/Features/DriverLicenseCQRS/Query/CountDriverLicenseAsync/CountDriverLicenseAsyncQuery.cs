using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.CountDriverLicenseAsync;

public class CountDriverLicenseAsyncQuery : IRequest<int>
{
    public ISpecification<DriverLicense> specification { get; set; }

    public CountDriverLicenseAsyncQuery(ISpecification<DriverLicense> specification)
    {
        this.specification = specification;
    }
}