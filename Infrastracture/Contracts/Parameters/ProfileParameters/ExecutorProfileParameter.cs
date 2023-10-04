using Infrastracture.Contracts.Specifications;

namespace Infrastracture.Contracts.Parameters.ProfileParameters;

public class ExecutorProfileParameter
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 12;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public long? StepId { get; set; }
    public long? AreaId { get; set; }
    public long? RequestedUserId { get; set; }
    public long? ConfirmedUserId { get; set; }
    
    public List<int>? RequestedStatus { get; set; }

    
    public List<int> Status { get; set; }
}