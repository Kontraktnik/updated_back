namespace Domain.Models.StepModels;

public class StepGroup : BaseModel
{
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    public int Order { get; set; }
    
    public virtual ICollection<Step> Steps { get; set; }

}