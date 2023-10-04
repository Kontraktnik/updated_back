using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.UserModels;

namespace Domain.Models.SurveyModels;

public class SurveyExecutor : BaseModel
{
    
    public long SurveyId { get; set; }
    public virtual Survey Survey { get; set; }
    
    [ForeignKey("User")]
    public long ExecutorId { get; set; }
    public virtual User Executor { get; set; }
    
    [ForeignKey("User")]
    public long DirectorId { get; set; }
    public virtual User Director { get; set; }
    
}