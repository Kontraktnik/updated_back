namespace Infrastracture.Contracts.Parameters.UserParameters;

public class UserParameter
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 12;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    
    
    public long? RoleId { get; set; }
    
    public long? AreaId { get; set; }

    public bool? Verified { get; set; }
    
    public bool? Status { get; set; }
    
    public string? Search { get; set; } //SEARCH BY IIN PHONE FULLANAME EMAIL
}