using Domain.Models.UserModels;

namespace Domain.Models.NotificationModels;

public class PhoneNotification : BaseModel
{
    
    public string Code { get; set; }
    
    public long UserId { get; set; }
    
    public virtual User User { get; set; }
    
    public string Phone { get; set; }
    
    public bool Status { get; set; }
    
    public string Purpose { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ExpiredAt { get; set; }
    
    
}