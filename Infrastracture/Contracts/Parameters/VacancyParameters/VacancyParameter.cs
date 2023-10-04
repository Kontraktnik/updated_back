namespace Infrastracture.Contracts.Parameters.VacancyParameters;

public class VacancyParameter
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
    public long? PositionId { get; set; }
    public List<long>? AreaId { get; set; } 
    public List<long>? ArmyTypeId { get; set; } 
    public List<long>? ArmyRegionId { get; set; } 
}