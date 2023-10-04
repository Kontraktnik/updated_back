namespace Application.DTO.DigitalSign;

public class SigningDataDTO
{
    /// <summary>
    /// Атрибуты
    /// </summary>
    public List<SigningAttributeDTO> Attributes { get; set; } = new List<SigningAttributeDTO>();

    /// <summary>
    /// Вложения
    /// </summary>
    public List<AttachedFileDTO> Attachments { get; set; } = new List<AttachedFileDTO>();

    /// <summary>
    /// Цифровые подписи
    /// </summary>
    public List<SigningEdsDTO> Edses { get; set; } = new List<SigningEdsDTO>();

    /// <summary>
    /// URL для получения массива подписываемых данных
    /// </summary>
    public string? DocumentItemArrayUrl { get; set; }
    public byte[] MessageDigest { get; set; }
}
