namespace Application.DTO.Calculation.JobYearDTO;

public class JobYearRDTO
{
    public long Id { get; set; }
    public long JobCategoryId { get; set; }
    public virtual JobCategoryDTO JobCategory { get; set; }
    
    public long ServiceYearId { get; set; }
    public virtual ServiceYearDTO ServiceYear { get; set; }

    public int Salary { get; set; }
}