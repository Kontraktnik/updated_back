namespace Application.DTO.DigitalSign;

public class DigitalSignBaseDTO
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
    public long BinaryDataId { get; set; }
    public long? SurveyId { get; set; }
    public long? WhoSignedId { get; set; }
}
