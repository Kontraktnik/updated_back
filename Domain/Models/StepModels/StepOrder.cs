using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.StepModels;

public class StepOrder : BaseModel
{
    public long StepId { get; set; }
    public Step Step { get; set; }
    
    [ForeignKey("PreviousStep")]
    public long? PreviousStepId { get; set; }
    public virtual Step? PreviousStep { get; set; }
    
    [ForeignKey("NextStep")]
    public long? NextStepId { get; set; }
    public virtual Step? NextStep { get; set; }
    
    
    
}