namespace Application.Enums;

/// <summary>
/// Статусы проверки сертификата пользователя
/// </summary>
public enum CertificateStatusEnum
{
    /// <summary>
    /// Неизвестен - значение по умолчанию
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Действительный
    /// </summary>
    Valid = 1,
    /// <summary>
    /// Отозван
    /// </summary>
    Revoked = 2,
    /// <summary>
    /// Истек
    /// </summary>
    Expired = 3,
    /// <summary>
    /// Еще не действителен (дата начала действия сертификата больше текущей)
    /// </summary>
    NotYetValid = 4
}
