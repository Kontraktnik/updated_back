namespace Application.DTO.Step;

public class StepUpdateDTO
{
    public long Id { get; set; }
    
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    public int DayLimit { get; set; }

}