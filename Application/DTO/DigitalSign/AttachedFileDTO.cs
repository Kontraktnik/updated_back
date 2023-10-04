namespace Application.DTO.DigitalSign;

public class AttachedFileDTO
{
    /// <summary>
    /// Наименование файла
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Путь к файлу
    /// </summary>
    public string? FileUrl { get; set; }

    /// <summary>
    /// Расширение файла
    /// </summary>
    public string? Extention { get; set; }

    /// <summary>
    /// Размер файла в байтах
    /// </summary>
    public long? FileSize { get; set; }

    /// <summary>
    /// Количество страниц
    /// </summary>
    public int? PagesCount { get; set; }
}
