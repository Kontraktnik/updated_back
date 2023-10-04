using Application.DTO.DigitalSign;

namespace Application.Contracts.Service;

public interface ICryptoService
{
    Task<CertStatusInfoDTO> ValidateCertificate(byte[] certificateBinary);

    /// <summary>
    /// Получить хэш данных согласно алгоритму
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    byte[] GetMessageDigest(byte[] data);

    /// <summary>
    /// Получить информацию об ЭЦП
    /// </summary>
    /// <param name="binaryData">Подписанные данные</param>
    /// <returns></returns>
    DigitalSignInfoDTO GetDigitalSignInfo(byte[] binaryData);

    /// <summary>
    /// Получить названия подписанных файлов
    /// </summary>
    /// <param name="binaryData"></param>
    /// <returns></returns>
    string[] GetSignedFileNames(byte[] binaryData);

    /// <summary>
    /// Проверка ЭЦП
    /// </summary>
    /// <param name="signedContent"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    DigitalSignVerifyResultDTO Verify(byte[] signedContent, byte[] sign);

}
