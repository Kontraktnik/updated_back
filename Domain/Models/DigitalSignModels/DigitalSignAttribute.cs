namespace Domain.Models.DigitalSignModels;

public class DigitalSignAttribute : BaseModel
{
    public int AttributeType { get; set; }
    public int Order { get; set; }
    public string FieldName { get; set; }
    public string DisplayName { get; set; }
    public string? Prefix { get; set; }
    public int? Type { get; set; }
}
