namespace Infrastracture.Contracts.Parameters.SurveyRepositories;

public class SurveyForManagementParameters
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 12;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public string? Search { get; set; }
    
    public long? EducationId { get; set; }
   
    public long? ArmyRankId { get; set; }
   
    public long? VTShId { get; set; }
   
    public long? PositionId { get; set; }
    
    public int? Status { get; set; }
    
    public long? StepId { get; set; }
    
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}