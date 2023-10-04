using Application.DTO.System;

namespace Application.DTO.Calculation.PositionDTO;

public class PositionRDTO
{
    public long Id { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    //1-6
    public long JobCategoryId { get; set; }
    public  JobCategoryDTO JobCategory { get; set; }
    //1-3
    public long SecretLevelId { get; set; }
    public  SecretLevelDTO SecretLevel { get; set; }
    //ArmyType
    public long? ArmyTypeId { get; set; }
    public virtual ArmyTypeDTO? ArmyType { get; set; }
    
    public long? CategoryPositionId { get; set; }
    public virtual CategoryPositionDTO? CategoryPosition { get; set; }
}