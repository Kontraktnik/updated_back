using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;

namespace Domain.Models.VacancyModel;

public class Vacancy : BaseModel
{
    public long PositionId { get; set; }
    public Position Position { get; set; }
    
    public long JobCategoryId { get; set; }
    public virtual JobCategory JobCategory { get; set; }
    
    public long AreaId { get; set; }
    public virtual Area Area { get; set; }
    
    public string City { get; set; }

    public long ArmyTypeId { get; set; }
    public virtual ArmyType ArmyType { get; set; }
    
    public long ArmyRegionId { get; set; }
    public virtual ArmyRegion ArmyRegion { get; set; }
    [ForeignKey("SecretLevel")]
    public long SecretLevelId { get; set; }
    public SecretLevel SecretLevel { get; set; }
    
    public long QualificationId { get; set; }
    public Qualification Qualification { get; set; }
    
    public long? AuthorId { get; set; }
    public virtual User? Author { get; set; }
    
    public int Quantity { get; set; }
    public bool Status { get; set; } = true;
    
    public string DescriptionRu { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionKz { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }



}