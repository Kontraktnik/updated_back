namespace Application.DTO.Step;

public class StepDTO
{
    public long Id { get; set; }
    
    public long StepGroupId { get; set; }
    public StepGroupDTO StepGroup { get; set; }
    
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }

    public long RequestedRoleId { get; set; }
    
    public long ConfirmedRoleId { get; set; }
    
    public bool IsFirst { get; set; }
    public bool IsLast { get; set; }
    
    public int DayLimit { get; set; }
    
    public virtual ICollection<StepOrderDTO> StepOrders { get; set; }

}