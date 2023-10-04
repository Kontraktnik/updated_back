namespace Domain.Models.CalculationModels;

public class JobYear : BaseModel
{
    public long JobCategoryId { get; set; }
    public virtual JobCategory JobCategory { get; set; }
    
    public long ServiceYearId { get; set; }
    public virtual ServiceYear ServiceYear { get; set; }

    public int Salary { get; set; }

    
}