using System.Text.Json.Serialization;

namespace Domain.Models.DigitalSignModels;

public class DigitalSignInfo : BaseModel
{
    /// <summary>
    /// Дата начала действия сертификата
    /// </summary>
    public DateTime NotBefore { get; set; }

    /// <summary>
    /// Дата окончания действия сертификата
    /// </summary>
    public DateTime NotAfter { get; set; }

    /// <summary>
    /// ИИН
    /// </summary>
    public string? Iin { get; set; }
    /// <summary>
    /// Наименование организации
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// БИН
    /// </summary>
    public string? Bin { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Тип пользователя сертификата
    /// </summary>
    public string? UserType { get; set; }

    /// <summary>
    /// Издатель сертификата
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Серийный номер
    /// </summary>
    public string? SerialNumber { get; set; }


    [JsonIgnore]
    public virtual ICollection<DigitalSign> DigitalSigns { get; set; }
}
