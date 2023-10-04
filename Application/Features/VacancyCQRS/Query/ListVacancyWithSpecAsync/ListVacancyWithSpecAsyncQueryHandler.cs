using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Vacancy;
using AutoMapper;
using MediatR;

namespace Application.Features.VacancyCQRS.Query.ListVacancyWithSpecAsync;

public class ListVacancyWithSpecAsyncQueryHandler : IRequestHandler<ListVacancyWithSpecAsyncQuery,ResponseDTO<ICollection<VacancyRDTO>>>
{
    private IVacancyRepository _repository;
    private IMapper _mapper;
    public ListVacancyWithSpecAsyncQueryHandler(IVacancyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<VacancyRDTO>>> Handle(ListVacancyWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListWithSpecAsync(request.specification);
        
        return new ResponseDTO<ICollection<VacancyRDTO>>
        {
            Success = true,
            StatusCode = (int) HttpStatusCode.OK,
            Data = _mapper.Map<ICollection<VacancyRDTO>>(models)
        };
        
        
    }
}