namespace Application.DTO.Surveys.SurveyStatistics;

public class SurveyStatisticsAllDTO
{
    public int Count { get; set; }
    public List<SurveyStatisticsDTO> All { get; set; }
    public List<SurveyStatisticsDTO> ByStepId { get; set; }
}