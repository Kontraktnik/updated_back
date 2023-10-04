namespace Domain.Models.SystemModels;

public class MedicalStatus : BaseModel
{
    public string Code { get; set; }
    
    public string TitleRu { get; set; }
    public string TitleKz { get; set; }
    public string TitleEn { get; set; }
    
    
}