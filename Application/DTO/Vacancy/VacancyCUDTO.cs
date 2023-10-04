namespace Application.DTO.Vacancy;

public class VacancyCUDTO
{
    public long PositionId { get; set; }
    
    public long JobCategoryId { get; set; }
    
    public string City { get; set; }

    public long ArmyTypeId { get; set; }
    
    public long ArmyRegionId { get; set; }
    public long SecretLevelId { get; set; }
    
    public long QualificationId { get; set; }
    
    public int Quantity { get; set; }
    public bool Status { get; set; } = true;
    
    public string DescriptionRu { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionKz { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}