namespace Infrastracture.Contracts.Parameters.SecretLevelParameters;

public class SecretLevelParameter
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 12;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int? Percentage { get; set; }
    public string? Search { get; set; }
}