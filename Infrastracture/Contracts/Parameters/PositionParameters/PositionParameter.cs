namespace Infrastracture.Contracts.Parameters.PositionParameters;

public class PositionParameter
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
    public long? JobCategoryId { get; set; }
    public long? SecretId { get; set; }
    public long? ArmyTypeId { get; set; }
    public long? CategoryPositionId { get; set; }

}