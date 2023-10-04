namespace Infrastracture.Contracts.Parameters.JobYearParameters;

public class JobYearParameter
{
    private const int MaxPageSize = 100;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 12;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public long? JobCategoryId { get; set; }
    public long? ServiceYearId { get; set; }
    public int? Salary { get; set; }


}