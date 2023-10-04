using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Vacancy;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.VacancyCQRS.Query.GetVacancyByIdAsync;

public class GetVacancyByIdAsyncQueryHandler : IRequestHandler<GetVacancyByIdAsyncQuery,ResponseDTO<VacancyRDTO>>
{
    private IVacancyRepository _repository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetVacancyByIdAsyncQueryHandler(IVacancyRepository repository, IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _repository = repository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<VacancyRDTO>> Handle(GetVacancyByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var model = await _repository.GetEntityWithSpecAsync(request.specification);
        if (model != null)
        {
            return new ResponseDTO<VacancyRDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.OK,
                Data = _mapper.Map<VacancyRDTO>(model)
            };
        }
        else
        {
            return new ResponseDTO<VacancyRDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"]
            };
        }
        
        
    }
}