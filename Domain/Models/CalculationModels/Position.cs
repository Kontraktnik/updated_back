using Domain.Models.SystemModels;

namespace Domain.Models.CalculationModels;

public class Position : BaseModel
{
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    //1-6
    public long JobCategoryId { get; set; }
    public  JobCategory JobCategory { get; set; }
    //1-3
    public long SecretLevelId { get; set; }
    public  SecretLevel SecretLevel { get; set; }
    //ArmyType
    public long? ArmyTypeId { get; set; }
    public virtual ArmyType? ArmyType { get; set; }
    
    public long? CategoryPositionId { get; set; }
    public virtual CategoryPosition? CategoryPosition { get; set; }
    
    
    
    
}