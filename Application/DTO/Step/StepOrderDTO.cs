namespace Application.DTO.Step;

public class StepOrderDTO
{
    public long Id { get; set; }
    public long StepId { get; set; }
    public StepDTO Step { get; set; }
    
    public long? PreviousStepId { get; set; }
    public virtual StepDTO? PreviousStep { get; set; }
    
    public long? NextStepId { get; set; }
    public virtual StepDTO? NextStep { get; set; }

}