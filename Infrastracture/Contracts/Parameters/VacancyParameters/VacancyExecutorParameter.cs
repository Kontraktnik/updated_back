namespace Infrastracture.Contracts.Parameters.VacancyParameters;

public class VacancyExecutorParameter
{
    private const int MaxPageSize = 100;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 12;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    
    public string? Search { get; set; }
    public long? PositionId { get; set; }
    public long? JobCategoryId { get; set; }
    public long? ArmyTypeId { get; set; } 
    public long? ArmyRegionId { get; set; } 
    public long? SecretLevelId { get; set; } 
    public long? QualificationId { get; set; }
    

}