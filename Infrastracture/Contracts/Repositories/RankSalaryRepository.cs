using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using AutoMapper;
using Domain.Models.CalculationModels;
using Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Contracts.Repositories;

public class RankSalaryRepository : GenericRepository<RankSalary>, IRankSalaryRepository
{
    private AppDbContext _context;
    private IMapper _mapper;
    public RankSalaryRepository(AppDbContext context,IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<RankSalaryRDTO>> GetByRankId(long RankId)
    {
        var model = await _context.RankSalaries.AsNoTracking().Include(p=>p.ArmyRank).FirstOrDefaultAsync(p => p.ArmyRankId == RankId);
        if (model != null)
        {
            return new ResponseDTO<RankSalaryRDTO>()
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<RankSalaryRDTO>(model)
            };
        }
        else
        {
            return new ResponseDTO<RankSalaryRDTO>()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Not Found"
            };
        }
    }
}