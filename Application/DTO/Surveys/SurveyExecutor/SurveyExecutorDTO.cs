using Application.DTO.User;

namespace Application.DTO.Survey;

public class SurveyExecutorDTO
{
    public long SurveyId { get; set; }
    public virtual SurveyDTO Survey { get; set; }
    
    public long ExecutorId { get; set; }
    public virtual UserDTO Executor { get; set; }
    
    public long DirectorId { get; set; }
    public virtual UserDTO Director { get; set; }
}