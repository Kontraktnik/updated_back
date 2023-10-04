namespace Application.DTO.Calculation;

public class ServiceYearDTO
{
    public long Id { get; set; }
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    public int Max { get; set; }
    public int Min { get; set; }
}