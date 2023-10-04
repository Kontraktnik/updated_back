using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.UserModels;

namespace Domain.Models.DigitalSignModels;

public class DigitalSign : BaseModel
{
    public string? FileName { get; set; }

    public DateTime Signed { get; set; }

    /// <summary>
    /// дата и время проверки подписи
    /// </summary>
    public DateTime? Verified { get; set; }

    /// <summary>
    /// Подпись верна
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Сообщение об ошибке (при проверке подписи)
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Подпись отклонена (задание было отозвано или другая причина)
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// ИД записи с данными об ЭЦП
    /// </summary>
    public long? InfoId { get; set; }

    public virtual DigitalSignInfo? Info { get; set; }
    public long BinaryDataId { get; set; }
    public virtual DigitalSignBinary BinaryData { get; set; }
    public long? SurveyId { get; set; }
    public virtual Survey? Survey { get; set; }
    public long? StepId { get; set; }
    public virtual Step? Step { get; set; }
    public long? WhoSignedId { get; set; }
    public virtual User? SignedUser { get; set; }
}
