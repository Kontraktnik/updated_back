using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Domain.Models.DigitalSignModels;
using Domain.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models.StepModels;

public class Step : BaseModel
{
    public long StepGroupId { get; set; }
    public StepGroup StepGroup { get; set; }
    
    public string TitleRu { get; set; }
    public string TitleEn { get; set; }
    public string TitleKz { get; set; }
    
    [ForeignKey("Role")]
    public long RequestedRoleId { get; set; }
    public virtual Role RequestedRole { get; set; }
    
    [ForeignKey("Role")]
    public long ConfirmedRoleId { get; set; }
    public virtual Role ConfirmedRole { get; set; }


    public bool IsFirst { get; set; }
    public bool IsLast { get; set; }
    
    public int DayLimit { get; set; }

    [JsonIgnore]
    public virtual ICollection<DigitalSign> DigitalSigns { get; set; }
}