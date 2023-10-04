using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.System;
using Application.DTO.User;

namespace Application.DTO.Vacancy;

public class VacancyRDTO
{
    public long Id { get; set; }
    public long PositionId { get; set; }
    public virtual PositionRDTO Position { get; set; }
    
    public long JobCategoryId { get; set; }
    public virtual JobCategoryDTO JobCategory { get; set; }
    
    public long AreaId { get; set; }
    public virtual AreaDTO Area { get; set; }
    
    public string City { get; set; }

    public long ArmyTypeId { get; set; }
    public virtual ArmyTypeDTO ArmyType { get; set; }
    
    public long ArmyRegionId { get; set; }
    public virtual ArmyRegionDTO ArmyRegion { get; set; }
    public long SecretLevelId { get; set; }
    public SecretLevelDTO SecretLevel { get; set; }
    
    public long QualificationId { get; set; }
    public QualificationDTO Qualification { get; set; }
    
    public long? AuthorId { get; set; }
    public virtual UserDTO? Author { get; set; }
    
    public int Quantity { get; set; }
    public bool Status { get; set; } = true;
    
    public string DescriptionRu { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionKz { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}