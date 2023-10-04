namespace Application.DTO.DigitalSign;

public class DigitalSignInputDTO
{

    /// <summary>
    /// Имя файла (для отправки и загрузки)
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Подписано (дата и время)
    /// </summary>
    public DateTime Signed { get; set; }

    /// <summary>
    /// Подписано UTC
    /// </summary>
    public DateTime SignedUtc { get; set; }

    /// <summary>
    /// Поле из внешней системы
    /// </summary>
    public string? ExternalSystemField { get; set; }

    public long? SurveyId { get; set; }
}
