namespace Application.DTO.Calculation;

public class QualificationDTO
{
    public long Id { get; set; }

    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    public int Percentage { get; set; }
}