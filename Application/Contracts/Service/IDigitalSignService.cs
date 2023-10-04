using Application.DTO.DigitalSign;

namespace Application.Contracts.Service;

public interface IDigitalSignService
{
    /// <summary>
    /// Получить подписываемые данные
    /// </summary>
    /// <param name="surveyId">Идентификатор заявки</param>
    /// <param name="iin">ИИН заявителя</param>
    /// <param name="fileNames">Названия подписанных файлов</param>
    /// <returns></returns>
    Task<SigningDataDTO?> GetSigningData(long surveyId, string? iin, string[]? fileNames = null);

    /// <summary>
    /// Сохранить подписанные данные
    /// </summary>
    /// <param name="info">Инфо о подписи</param>
    /// <param name="binaryData">Подписанные данные</param>
    /// <param name="iin">ИИН текущего пользователя</param>
    /// <returns></returns>
    Task<long?> SaveSignBase64(DigitalSignInputDTO? info, byte[] binaryData, string? iin);

}
