namespace Domain.Models.SystemModels;

public class Area : BaseModel
{
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    public int? RegionNumber { get; set; }
}