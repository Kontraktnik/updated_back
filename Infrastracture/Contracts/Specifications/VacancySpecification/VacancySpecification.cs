using Domain.Models.VacancyModel;
using Infrastracture.Contracts.Parameters.VacancyParameters;

namespace Infrastracture.Contracts.Specifications.VacancySpecification;

public class VacancySpecification : BaseSpecification<Vacancy>
{
    public VacancySpecification()
    {
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("ArmyType");
        AddInclude("ArmyRegion");
    } 
    public VacancySpecification(VacancyParameter parameter,bool Status) : base(
        p=>
            (
               (p.Status.Equals(Status)) &&
               (!parameter.PositionId.HasValue || p.PositionId.Equals(parameter.PositionId)) &&
               (string.IsNullOrEmpty(parameter.Search) ||
                 p.City.ToLower().Contains(parameter.Search.ToLower())) &&
               (parameter.AreaId == null || parameter.AreaId.Count == 0 || parameter.AreaId.Contains(p.AreaId)) &&
               (parameter.ArmyRegionId == null || parameter.ArmyRegionId.Count == 0 || parameter.ArmyRegionId.Contains(p.ArmyRegionId)) &&
               (parameter.ArmyTypeId == null || parameter.ArmyTypeId.Count == 0 || parameter.ArmyTypeId.Contains(p.ArmyTypeId))
               
            )
        )
    {
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("ArmyType");
        AddInclude("ArmyRegion");
        AddOrderByDescending(p=>p.CreatedAt);
        
        ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);

    }
    public VacancySpecification(long Id,bool Status = true,string? defaultValue = null) : base(p=>p.Id.Equals(Id) && p.Status == Status )
    {
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("ArmyType");
        AddInclude("ArmyRegion");
    }
    
    
}

public class CountVacancySpecification : BaseSpecification<Vacancy>
{
    public CountVacancySpecification()
    {
        
    }
    
    public CountVacancySpecification(VacancyParameter parameter,bool Status) : base(
        p=>
        (
            (p.Status.Equals(Status)) &&
            (!parameter.PositionId.HasValue || p.PositionId.Equals(parameter.PositionId)) &&
            (string.IsNullOrEmpty(parameter.Search) ||
             p.DescriptionRu.ToLower().Contains(parameter.Search.ToLower()) ||
             p.DescriptionKz.ToLower().Contains(parameter.Search.ToLower()) ||
             p.DescriptionEn.ToLower().Contains(parameter.Search.ToLower())) &&
            (parameter.AreaId == null || parameter.AreaId.Count == 0 || parameter.AreaId.Contains(p.AreaId)) &&
            (parameter.ArmyRegionId == null || parameter.ArmyRegionId.Count == 0 || parameter.ArmyRegionId.Contains(p.ArmyRegionId)) &&
            (parameter.ArmyTypeId == null || parameter.ArmyTypeId.Count == 0 || parameter.ArmyTypeId.Contains(p.ArmyTypeId))
        )
    )
    {
        

    }
}



public class VacancyExecutorSpecification : BaseSpecification<Vacancy>
{
    public VacancyExecutorSpecification()
    {
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("ArmyType");
        AddInclude("ArmyRegion");
    } 
    public VacancyExecutorSpecification(VacancyExecutorParameter parameter,long AreaId,bool isPaging = true) : base(
        p=>
        (
            (p.AreaId.Equals(AreaId)) &&
            (!parameter.PositionId.HasValue || p.PositionId.Equals(parameter.PositionId)) &&
            (string.IsNullOrEmpty(parameter.Search) ||
             p.DescriptionRu.ToLower().Contains(parameter.Search.ToLower()) ||
             p.DescriptionKz.ToLower().Contains(parameter.Search.ToLower()) ||
             p.DescriptionEn.ToLower().Contains(parameter.Search.ToLower())) &&
             (!parameter.PositionId.HasValue || p.PositionId.Equals(parameter.PositionId))&&
            (!parameter.JobCategoryId.HasValue || p.JobCategoryId.Equals(parameter.JobCategoryId))&&
            (!parameter.ArmyTypeId.HasValue || p.ArmyTypeId.Equals(parameter.ArmyTypeId))&&
            (!parameter.ArmyRegionId.HasValue || p.ArmyRegionId.Equals(parameter.ArmyRegionId))&&
            (!parameter.SecretLevelId.HasValue || p.SecretLevelId.Equals(parameter.SecretLevelId))&&
            (!parameter.QualificationId.HasValue || p.QualificationId.Equals(parameter.QualificationId))
        )
    )
    {
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("ArmyType");
        AddInclude("ArmyRegion");
        AddInclude("JobCategory");
        AddInclude("SecretLevel");
        AddInclude("Qualification");
        AddOrderByDescending(p=>p.CreatedAt);
        if (isPaging)
        {
            ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
        }    

    }
    public VacancyExecutorSpecification(long Id,long AreaId,string? defaultValue = null) : base(p=>p.Id.Equals(Id) && p.AreaId == AreaId  )
    {
        AddInclude("Position");
        AddInclude("Area");
        AddInclude("ArmyType");
        AddInclude("ArmyRegion");
        AddInclude("JobCategory");
        AddInclude("SecretLevel");
        AddInclude("Qualification");
    }
    
    
}