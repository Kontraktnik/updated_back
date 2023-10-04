namespace Application.DTO.Calculation.PositionDTO;

public class PositionCUDTO
{
    public long Id { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    public long JobCategoryId { get; set; }
    public long SecretLevelId { get; set; }
    public long? ArmyTypeId { get; set; }
    public long? CategoryPositionId { get; set; }
}