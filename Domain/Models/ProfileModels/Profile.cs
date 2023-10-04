using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;

namespace Domain.Models.ProfileModels;

public class Profile : BaseModel
{
    public long StepGroupId { get; set; }
    public StepGroup StepGroup { get; set; }
    
    public long StepId { get; set; }
    public Step Step { get; set; }
    
    public long SurveyId { get; set; }
    public virtual Survey Survey { get; set; }
    
    public long AreaId { get; set; }
    public virtual Area Area { get; set; }
    
    [ForeignKey("User")]
    public long RequestedUserId { get; set; }
    public virtual User RequestedUser { get; set; }
    
    public string RequestedUserIIN { get; set; }
    public int RequestedStatus { get; set; } 
    public string RequestedSIGN { get; set; }
    
    [ForeignKey("User")]
    public long? ConfirmedUserId { get; set; }
    public virtual User? ConfirmedUser { get; set; }
    
    public string? ConfirmedUserIIN { get; set; }
    public int? ConfirmedStatus { get; set; } 
    public string? ConfirmedSIGN { get; set; }
    
    public int Status { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProfileFile> ProfileFiles { get; set; }

}