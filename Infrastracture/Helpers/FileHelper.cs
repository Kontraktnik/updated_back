namespace Infrastracture.Helpers;

public class FileHelper
{
    /// <summary>
    /// Получить байты из потока
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static byte[] ReadFully(Stream input)
    {
        using MemoryStream ms = new();
        input.CopyTo(ms);
        return ms.ToArray();
    }

    /// <summary>
    /// Получить название файла
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <returns></returns>
    public static string GetFileName(string filePath)
    {
        return Path.GetFileName(filePath);
    }

    /// <summary>
    /// Получить расширение файла
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <returns></returns>
    public static string GetFileExtention(string filePath)
    {
        return Path.GetExtension(filePath);
    }

    /// <summary>
    /// Получить размер файла
    /// </summary>
    /// <param name="filePath">путь к файлу</param>
    /// <returns></returns>
    public static long GetFileSize(string filePath)
    {
        return new FileInfo(filePath).Length;
    }
}
