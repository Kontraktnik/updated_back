using System.Text.Json.Serialization;

namespace Domain.Models.DigitalSignModels;

public class DigitalSignBinary : BaseModel
{

    /// <summary>
    /// Массив байт подписи
    /// </summary>
    public byte[] Data { get; set; }


    [JsonIgnore]
    public virtual ICollection<DigitalSign> DigitalSigns { get; set; }
}
