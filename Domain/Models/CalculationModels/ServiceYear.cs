namespace Domain.Models.CalculationModels;

public class ServiceYear : BaseModel
{
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    public int Max { get; set; }
    public int Min { get; set; }
}