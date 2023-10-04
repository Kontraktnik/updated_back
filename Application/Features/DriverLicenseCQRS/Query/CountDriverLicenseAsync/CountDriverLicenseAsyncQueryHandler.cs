using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.DriverLicenseCQRS.Query.CountDriverLicenseAsync;

public class CountDriverLicenseAsyncQueryHandler : IRequestHandler<CountDriverLicenseAsyncQuery,int>
{
    private readonly IDriverLicenseRepository _driverLicenseRepository;
    private readonly IMapper _mapper;

    public CountDriverLicenseAsyncQueryHandler(IDriverLicenseRepository driverLicenseRepository,IMapper mapper)
    {
        _driverLicenseRepository = driverLicenseRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountDriverLicenseAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _driverLicenseRepository.CountAsync(request.specification);
    }
}