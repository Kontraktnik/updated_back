namespace Domain.Models.CalculationModels;

public class Qualification : BaseModel
{
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    public int Percentage { get; set; }
}