using Application.DTO.Profile;
using Application.DTO.Survey;

namespace Application.DTO.EmailDTO;

public class Email
{
    public Email()
    {
        
    }
    
    
    
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}