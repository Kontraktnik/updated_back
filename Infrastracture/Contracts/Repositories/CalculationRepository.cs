using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Contracts.Repositories;

public class CalculationRepository : ICalculationRepository
{
    private readonly AppDbContext _context;
    public CalculationRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseDTO<int>> CountSalary(CalculationDTO model)
    {
        try
        {
            var position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == model.PositionId);
            if (position != null)
            {
                var year_job = await _context.JobYears
                    .Where(p => (p.JobCategoryId == position.JobCategoryId && p.ServiceYearId == model.ServiceYearId))
                    .FirstOrDefaultAsync();
                if (year_job != null)
                {
                    var hello = "";
                    var salary = year_job.Salary;
                    var qualification = await _context.Qualifications.FirstOrDefaultAsync(p => p.Id == model.QualificationId);
                    if (qualification != null)
                    {
                        var q = year_job.Salary / 100 * qualification.Percentage;
                        salary += year_job.Salary / 100 * qualification.Percentage;
                        hello = q.ToString();
                    }

                    var secret_level =
                        await _context.SecretLevels.FirstOrDefaultAsync(p => p.Id == position.SecretLevelId);

                    if (secret_level != null)
                    {
                        salary += year_job.Salary / 100 * secret_level.Percentage;
                        
                    }

                    var rank_salary =
                        await _context.RankSalaries.FirstOrDefaultAsync(p => p.ArmyRankId == model.ArmyRankId);
                    if (rank_salary != null)
                    {
                        salary += rank_salary.Salary;

                    }
                    var areasSalaries = AreaPopularSalary.GetAreaPopularSalaries();
                    if(model.AreaId != null && model.PersonQuantity != null)
                    {
                       var areasSalary = areasSalaries.Where(p => (p.AreaId == model.AreaId && p.PersonQuntity == model.PersonQuantity)).First();
                        if (areasSalary != null)
                        {
                            salary += areasSalary.Price;
                        }
                      
                    }
                    
                    return new ResponseDTO<int>()
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = hello,
                        Data = salary
                    };
                    
                }
                else
                {
                    return new ResponseDTO<int>()
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "По данной специльности не найдена информации",
                        Data = 0
                    };
                }
            }
            else
            {
                return new ResponseDTO<int>()
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "По данной специльности не найдена",
                    Data = 0
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<int>()
            {
                Success = false,
                StatusCode = 500,
                Message = ex.Message,
                Data = 0
            };
        }
        
    }

    public async Task<ResponseDTO<AreaSalary[]>> GetSalaryArea()
    {
        try
        {
            AreaSalary[] areaSalaries = AreaSalary.GetAllSalariesArea();
            return new ResponseDTO<AreaSalary[]>()
            {
                Success = true,
                StatusCode = 200,
                Data = areaSalaries
            };
        }
        catch(Exception ex)
        {
            return new ResponseDTO<AreaSalary[]>()
            {
                Success = false,
                StatusCode = 500,
                Message = ex.Message,
                Data = null
            };
        }
    }
}