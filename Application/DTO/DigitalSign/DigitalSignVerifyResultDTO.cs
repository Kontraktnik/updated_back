using Application.Enums;

namespace Application.DTO.DigitalSign;

public class DigitalSignVerifyResultDTO
{
    /// <summary>
    /// Проверена
    /// </summary>
    public DateTime Verified { get; set; }

    /// <summary>
    /// Подпись верна
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Сообщение об ошибке (при проверке подписи)
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Статус сертификата пользователя
    /// </summary>
    public CertificateStatusEnum CertificateStatus { get; set; }

    public string SledBase64 { get; set; }
    public string SignBase64 { get; set; }
    public long? DigitalSignId { get; set; }
}
