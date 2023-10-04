namespace Application.DTO.Step;

public class StepGroupDTO
{
    public long Id { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    public int Order { get; set; }
    
    public virtual ICollection<StepDTO> Steps { get; set; }
}