namespace Application.DTO.DigitalSign;

public class SigningEdsDTO
{
    /// <summary>
    /// Кем подписано
    /// </summary>
    public string WhoSigned { get; set; }

    /// <summary>
    /// Дата подписания
    /// </summary>
    public DateTime Signed { get; set; }

    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName { get; set; }
}
