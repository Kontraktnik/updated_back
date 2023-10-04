using Application.Contracts.Service;
using Application.DTO.DigitalSign;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Collections;
using Kz.GammaTech.Tsp;
using Kz.GammaTech.Asn1.Gamma;
using Kz.GammaTech.X509;
using Kz.GammaTech.Ocsp;
using Kz.GammaTech.Asn1.Ocsp;
using Kz.GammaTech.Crypto;
using Kz.GammaTech.Security;
using Kz.GammaTech.Asn1.X509;
using Kz.GammaTech.Asn1;
using Kz.GammaTech.Math;
using Kz.GammaTech.Cms;
using Kz.GammaTech.X509.Store;
using Kz.GammaTech.Crypto.Digests;
using Infrastracture.Helpers;
using Application.Enums;
using Kz.GammaTech.Security.Certificates;

namespace Infrastracture.Contracts.Service;

public class CryptoService : ICryptoService
{
    private readonly ILogger<CryptoService> _logger;
    private readonly CryptoConf _cryptoConfig;
    private static string OID_OCSP_SIGNING = "1.3.6.1.5.5.7.3.9";
    private static string OID_TIMESTAMP_SIGNING = "1.3.6.1.5.5.7.3.8";

    public CryptoService(ILogger<CryptoService> logger, IOptions<CryptoConf> cryptoConfig)
    {
        _logger = logger;
        _cryptoConfig = cryptoConfig.Value;
    }

    #region Validate certificate

    public async Task<CertStatusInfoDTO> ValidateCertificate(byte[] certificateBinary)
    {
        CertStatusInfoDTO certStatusInfo = new();

        try
        {
            DateTime currentDate = DateTime.Now.ToUniversalTime();
            X509Certificate certificate = new X509CertificateParser().ReadCertificate(certificateBinary);

            //Получение корневого сертификата
            X509Certificate rootX509Certificate = new X509CertificateParser()
                .ReadCertificate(await File.ReadAllBytesAsync(_cryptoConfig?.UCGORootCertificatePath ?? "/certs/UCGO.cer"));

            if (rootX509Certificate == null)
            {
                certStatusInfo.IsValid = false;
                certStatusInfo.ErrorInfo = "Не найден корневой сертификат.";
                return certStatusInfo;
            }

            if (IsCertificateExpired(rootX509Certificate, currentDate))
            {
                certStatusInfo.IsValid = false;
                certStatusInfo.ErrorInfo = "Ошибка проверки срока действительности корневого сертификата.";
                return certStatusInfo;
            }

            if (IsCertificateExpired(certificate, currentDate))
            {
                certStatusInfo.IsValid = false;
                certStatusInfo.IsExpired = true;
                certStatusInfo.ErrorInfo = "Срок действия сертификата истек либо не наступил.";
                return certStatusInfo;
            }

            if ((certificate.NotAfter - currentDate).TotalDays < 30)
            {
                certStatusInfo.ExpireDaysCount = (int)(certificate.NotAfter - currentDate).TotalDays;
            }

            //Проверка цепочки сертификата
            certStatusInfo.IsValid = VerifySign(
                certificate.GetTbsCertificate(), certificate.GetSignature(),
                rootX509Certificate.GetPublicKey(), certificate.SigAlgOid);

            // Если сертификат еще действителен, проверка назначения сертификата
            if (certStatusInfo.IsValid)
            {
                certStatusInfo.IsValid = CheckKeyUsage(certificate);

                if (!certStatusInfo.IsValid)
                {
                    return new CertStatusInfoDTO()
                    {
                        IsValid = false,
                        ErrorInfo = "Выбран неверный сертификат для подписи!"
                    };
                }
            }

            // Если сертификат еще действителен, проверка на отозванность
            if (certStatusInfo.IsValid)
            {
                try
                {
                    byte[] ocspResponse = await SendOcspRequest(certificate, rootX509Certificate);
                    var ocspResponseStatus = CheckOcspResponse(ocspResponse, certStatusInfo, currentDate, rootX509Certificate);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Ошибка при отправке запроса OCSP.");
                    certStatusInfo.IsValid = false;
                    certStatusInfo.ErrorInfo = "Ошибка при отправке запроса OCSP.";
                    return certStatusInfo;
                }

                try
                {
                    certStatusInfo.TimeStampResponse = await GetTimeStamp(certificate, _cryptoConfig?.TimeStampUrl, currentDate, rootX509Certificate);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Ошибка при отправке запроса TimeStamp.");
                }

                certStatusInfo.Iin = GetCertificateIin(certificate);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при поиске корневого сертификата: {ex.Message}");
        }

        return certStatusInfo;
    }

    /// <summary>
    /// Проверка подписи
    /// </summary>
    /// <param name="buf"></param>
    /// <param name="signBuf"></param>
    /// <param name="pubKey"></param>
    /// <param name="sigAlg"></param>
    /// <param name="isAuth"></param>
    /// <returns></returns>
    public bool VerifySign(byte[] buf, byte[] signBuf, AsymmetricKeyParameter pubKey, string sigAlg, bool isAuth = false)
    {
        _logger.LogInformation("Проверка подписи.");

        try
        {
            ISigner signer = SignerUtilities.GetSigner(sigAlg);
            signer.Init(false, pubKey);
            signer.Reset();
            signer.BlockUpdate(buf, 0, buf.Length);
            return signer.VerifySignature(signBuf);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Подпись не прошла проверку.");
            return false;
        }
    }

    /// <summary>
    /// Истек ли срок действия сертификата
    /// </summary>
    /// <param name="certificate"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    private bool IsCertificateExpired(X509Certificate certificate, DateTime date)
    {
        try
        {
            certificate.CheckValidity(date); // throws exception
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Срок действия сертификата {@certificate} истек либо не наступил", certificate);
        }
        return true;
    }

    /// <summary>
    /// Проверка назначения сертификата
    /// </summary>
    /// <param name="verifyCertificate"></param>
    /// <returns></returns>
    private bool CheckKeyUsage(X509Certificate verifyCertificate)
    {
        try
        {
            bool[] keyUsage = verifyCertificate.GetKeyUsage();
            if (keyUsage[0] != true || keyUsage[1] != true)
            {
                return false;
            }
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Возникла ошибка при проверке сертификата.");
            return false;
        }
    }

    /// <summary>
    /// Проверка применения сертификата
    /// </summary>
    /// <param name="certificate"></param>
    /// <param name="oid"></param>
    /// <returns></returns>
    private static bool CheckExtendedKeyUsage(X509Certificate certificate, string oid)
    {
        DerOctetString dEROctetString = (DerOctetString)certificate.GetExtensionValue(X509Extensions.ExtendedKeyUsage);
        DerSequence sequence = (DerSequence)Asn1Object.FromByteArray(dEROctetString.GetOctets());
        DerObjectIdentifier deroi = (DerObjectIdentifier)sequence.ToAsn1Object();

        return oid == deroi.Id;
    }

    /// <summary>
    /// Получить ИИН из сертификата
    /// </summary>
    /// <returns></returns>
    private static string GetCertificateIin(X509Certificate certificate)
    {
        var values = certificate.SubjectDN.GetValues();
        var oids = certificate.SubjectDN.GetOids();

        return values[oids.IndexOf(new DerObjectIdentifier("2.5.4.5"))].ToString().Remove(0, 3);
    }

    /// <summary>
    /// Отправка запроса в сервис OCSP
    /// </summary>
    /// <param name="x509Certificate"></param>
    /// <returns></returns>
    private async Task<byte[]> SendOcspRequest(X509Certificate x509Certificate, X509Certificate rootX509Certificate)
    {
        if (string.IsNullOrEmpty(_cryptoConfig?.OcspUrl))
        {
            throw new Exception("Не указан адрес службы OCSP.");
        }

        CertificateID id = new(CertificateID.HashGost, rootX509Certificate, x509Certificate.SerialNumber);

        OcspReqGenerator gen = new();
        gen.AddRequest(id);
        gen.SetRequestExtensions(СreateNonceExtensions());
        gen.SetRequestorName(x509Certificate.SubjectDN);
        X509Certificate[] chain = new X509Certificate[1];
        chain[0] = x509Certificate;

        byte[] ocspRequest = gen.Generate(GammaObjectIdentifiers.GostR3410x2001.Id, null, chain).GetEncoded();

        using HttpClient httpClient = new();
        httpClient.BaseAddress = new Uri(_cryptoConfig?.OcspUrl);
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Post,
            Content = new ByteArrayContent(ocspRequest)
        };
        request.Content.Headers.Add("content-type", "application/ocsp-request");
        HttpResponseMessage response = await httpClient.SendAsync(request);
        byte[] ocspResponse = await response.Content.ReadAsByteArrayAsync();
        _ = new OcspResp(ocspResponse);
        return ocspResponse;
    }

    /// <summary>
    /// Проверка ответа OCSP
    /// </summary>
    /// <param name="response"></param>
    /// <param name="certStatusInfo"></param>
    /// <param name="validateDate"></param>
    /// <returns></returns>
    private bool CheckOcspResponse(byte[] response, CertStatusInfoDTO certStatusInfo, DateTime validateDate, X509Certificate rootX509Certificate)
    {
        BasicOcspResp basicOCSPResponse = (BasicOcspResp)(new OcspResp(response)).GetResponseObject();
        SingleResp[] singleResp = basicOCSPResponse.Responses;

        if (singleResp.Length > 0)
        {
            CertStatus certStatus = (CertStatus)singleResp[0].GetCertStatus();
            X509Certificate x509certOCSP = new X509CertificateParser().ReadCertificate(certStatus.ToAsn1Object().GetEncoded());

            if (rootX509Certificate == null)
            {
                certStatusInfo.IsValid = false;
            }

            bool result = VerifySign(x509certOCSP.GetTbsCertificate(),
                x509certOCSP.GetSignature(), rootX509Certificate.GetPublicKey(),
                x509certOCSP.SigAlgOid);

            if (!result)
            {
                _logger.LogError("Подпись OCSP сертификата не действительна.");
                certStatusInfo.IsValid = false;
                certStatusInfo.ErrorInfo = "Подпись OCSP сертификата не действительна.";
                return false;
            }

            //Проверка применения сертификата (ExtendedKeyUsage)
            if (!CheckExtendedKeyUsage(x509certOCSP, OID_OCSP_SIGNING))
            {
                certStatusInfo.IsValid = false;
                return false;
            }

            // Проверка на срок действительности сертификата OCSP
            if (IsCertificateExpired(x509certOCSP, validateDate))
            {
                certStatusInfo.IsValid = false;
                return false;
            }

            // TODO проверка подписи ответа ocsp
            result = VerifySign(
                basicOCSPResponse.GetTbsResponseData(),
                basicOCSPResponse.GetSignature(),
                x509certOCSP.GetPublicKey(),
                basicOCSPResponse.SignatureAlgOid);
            if (!result)
            {
                certStatusInfo.IsValid = false;
                return false;
            }

            certStatusInfo.IsValid = true;
            int statusId = certStatus.TagNo;
            if (statusId == 0) // не отозван
            {
                certStatusInfo.IsRevoked = false;
            }
            else if (statusId == 1) // отозван
            {
                certStatusInfo.IsRevoked = true;
                RevokedInfo revokedInfo = (RevokedInfo)certStatus.Status;

                certStatusInfo.RevokedReasonCode = revokedInfo.RevocationReason.Value.IntValue;

                // TODO перенос в ресурсы
                // текст причины отзыва
                string[] revokeDescriptionsRu = {"Не указана", "Компрометация ключа",
                "Компрометация центра сертификации", "Изменена информация о владельце сертификата",
                "Замена ключа", "Досрочное прекращение действие ключа", "Действие сертификата приостановлено",
                "", "Возобновление действия сертификата", "Изъятие привилегий",
                "Компрометация аспектов атрибута органа по присвоению атрибутов"};

                string revokedReason = revokeDescriptionsRu[certStatusInfo.RevokedReasonCode];
                revokedReason ??= "Причина отзыва не известна";
                certStatusInfo.RevokedReason = revokedReason;

                // дата отзыва
                certStatusInfo.RevokedDate = (revokedInfo.RevocationTime.ToDateTime());
                string error = $"Сертификат отозван. Причина отзыва: {revokedReason}. Дата отзыва: {certStatusInfo.RevokedDate}"; ;
                _logger.LogError(error);
                certStatusInfo.ErrorInfo = error;
            }
            else if (statusId == 2) //не действителен, т.е. отсутствует в УЦ
            {
                string error = "Сертификат пользователя отсутствует в УЦ";
                _logger.LogError(error);
                certStatusInfo.ErrorInfo = error;
                certStatusInfo.IsValid = false;
            }

            certStatusInfo.OcspInfo = ($"OCSP квитанция (date: {basicOCSPResponse.ProducedAt} | Status: {statusId} | Signature: true | Certificate: {x509certOCSP.SubjectDN})");
            _logger.LogInformation(certStatusInfo.OcspInfo);
            certStatusInfo.OcspResponse = response;

            return true;
        }
        else
        {
            return false;
        }
    }

    private static X509Extensions СreateNonceExtensions()
    {
        BigInteger nonce = BigInteger.ValueOf(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        ArrayList oids = new()
        {
            OcspObjectIdentifiers.PkixOcspNonce
        };
        ArrayList values = new()
        {
            new X509Extension(false, new DerOctetString(nonce.ToByteArray()))
        };
        return new X509Extensions(oids, values);
    }

    /// <summary>
    /// Отправка и получение метки времени
    /// </summary>
    /// <param name="data"></param>
    /// <param name="url"></param>
    /// <param name="validateDate"></param>
    /// <returns></returns>
    public async Task<byte[]> GetTimeStamp(X509Certificate certificate, string url, DateTime validateDate, X509Certificate rootX509Certificate)
    {
        _logger.LogWarning("Получение метки времени.");

        TimeStampRequestGenerator reqGen = new();
        var tsp_req = reqGen.Generate(GammaObjectIdentifiers.GostR3411.Id, certificate.GetSignature()).GetEncoded();

        TimeStampResponse tsp_resp = await SendTimeStampRequest(tsp_req, url);

        if (!CheckTspResponse(tsp_resp.GetEncoded(), tsp_req, validateDate, rootX509Certificate))
        {
            _logger.LogError("Метка времени не действительна.");
            throw new Exception("Метка времени не действительна.");
        }

        return tsp_resp.GetEncoded();
    }

    /// <summary>
    /// Отправка запроса для получения метки времени
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    private async Task<TimeStampResponse> SendTimeStampRequest(byte[] hash, string url)
    {
        TimeStampRequest timeStampRequest = CreateTimeStampRequest(hash);
        HttpClient httpClient = new()
        {
            BaseAddress = new Uri(url)
        };
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Post,
            Content = new ByteArrayContent(timeStampRequest.GetEncoded())
        };
        request.Content.Headers.Add("content-type", "application/timestamp-query");
        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();
        TimeStampResponse tspResponse = new(content);
        return tspResponse;
    }

    /// <summary>
    /// Генерирование неподписанного TSP запроса
    /// </summary>
    /// <param name="hash"></param>
    /// <returns></returns>
    private static TimeStampRequest CreateTimeStampRequest(byte[] hash)
    {
        TimeStampRequestGenerator reqGen = new();
        reqGen.SetCertReq(true);
        reqGen.SetReqPolicy(new DerObjectIdentifier("1.2.3").Id);
        TimeStampRequest req = reqGen.Generate(TspAlgorithms.Gost3411, hash, BigInteger.ValueOf(100)); //Gost34311
        return req;
    }

    private bool CheckTspResponse(byte[] response, byte[] hash, DateTime validateDate, X509Certificate rootX509Certificate)
    {
        TimeStampToken tsToken = GetTimeStampToken(response);
        X509Certificate tspCertificate = GetTspCertificate(tsToken);

        if (rootX509Certificate == null)
        {
            throw new Exception("Не найден корневой сертификат.");
        }

        // Проверка данных TSP, т.е хэш от подписанных данных должен быть равен хэшу который в TSP
        if (!CheckTspData(tsToken, hash))
        {
            throw new Exception("Хэш TSP не прошел проверку.");
        }

        tsToken.Validate(tspCertificate);

        // Проверка на срок действительности сертификата
        if (IsCertificateExpired(tspCertificate, validateDate))
        {
            return false;
        };

        //Проверка применения сертификата (ExtendedKeyUsage)
        if (!CheckExtendedKeyUsage(tspCertificate, OID_TIMESTAMP_SIGNING))
        {
            throw new Exception("Сертификат не предназначен для подписания.");
        }

        // Проверка подписи TSP сертификата
        bool verifyResult = VerifySign(tspCertificate.GetTbsCertificate(), tspCertificate.GetSignature(),
                rootX509Certificate.GetPublicKey(), tspCertificate.SigAlgOid);

        if (!verifyResult)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Получение метки времени из ответа сервиса
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private TimeStampToken GetTimeStampToken(byte[] response)
    {
        TimeStampToken timeStampToken = null;
        try
        {
            timeStampToken = new TimeStampToken(new CmsSignedData(response));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "GetTimeStampToken");

            try
            {
                TimeStampResponse tspResponse = new(response);
                timeStampToken = tspResponse.TimeStampToken;
            }
            catch (Exception e2)
            {
                _logger.LogError(e2, "GetTimeStampToken");
            }
        }
        return timeStampToken;
    }

    /// <summary>
    /// Получение сертификата из метки времени
    /// </summary>
    /// <param name="tsToken"></param>
    /// <returns></returns>
    private static X509Certificate GetTspCertificate(TimeStampToken tsToken)
    {
        IX509Store certStore = tsToken.GetCertificates("Collection");
        ArrayList certificates = (ArrayList)certStore.GetMatches(null);
        X509Certificate cert = (X509Certificate)certificates[0];
        return cert;
    }

    /// <summary>
    /// Проверка данных TSP
    /// </summary>
    /// <param name="tsToken"></param>
    /// <param name="hash"></param>
    /// <returns></returns>
    private static bool CheckTspData(TimeStampToken tsToken, byte[] hash)
    {
        // Хеш на данные из TSP ответа
        byte[] digTSP = tsToken.TimeStampInfo.GetMessageImprintDigest();
        bool equals = Enumerable.SequenceEqual(digTSP, hash);
        return equals;
    }

    #endregion

    /// <summary>
    /// Получить хэш данных согласно алгоритму
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public byte[] GetMessageDigest(byte[] data)
    {
        _logger.LogWarning("Создание хэша данных");

        Gost3411Digest dig = new();
        dig.Reset();
        dig.BlockUpdate(data, 0, data.Length);
        byte[] hash = new byte[dig.GetDigestSize()];
        dig.DoFinal(hash, 0);

        _logger.LogWarning("Создан хэш данных.");
        return hash;
    }

    /// <summary>
    /// Получить информацию об ЭЦП
    /// </summary>
    /// <param name="binaryData">Подписанные данные</param>
    /// <returns></returns>
    public DigitalSignInfoDTO GetDigitalSignInfo(byte[] binaryData)
    {
        CmsSignedData cms = new(binaryData);
        var signers = cms.GetSignerInfos().GetSigners();

        IX509Store certs = cms.GetCertificates("Collection");

        DigitalSignInfoDTO digitalSignInfo = new();

        foreach (SignerInformation signerInformation in signers)
        {
            ArrayList certList = new(certs.GetMatches(signerInformation.SignerID));
            X509Certificate certificate = (X509Certificate)certList[0];

            digitalSignInfo.NotBefore = certificate.NotBefore;
            digitalSignInfo.NotAfter = certificate.NotAfter;
            digitalSignInfo.SerialNumber = certificate.SerialNumber.ToString();
            digitalSignInfo.Iin = GetCertificateIin(certificate);

            var subjectDnValues = certificate.SubjectDN.GetValues();
            var subjectDnOids = certificate.SubjectDN.GetOids();

            digitalSignInfo.Iin = subjectDnValues[subjectDnOids.IndexOf(new DerObjectIdentifier("2.5.4.5"))].ToString().Remove(0, 3);
            digitalSignInfo.FullName = subjectDnValues[subjectDnOids.IndexOf(new DerObjectIdentifier("2.5.4.3"))].ToString();

            if (subjectDnOids.IndexOf(new DerObjectIdentifier("2.5.4.10")) >= 0)
            {
                digitalSignInfo.Organization = subjectDnValues[subjectDnOids.IndexOf(new DerObjectIdentifier("2.5.4.10"))].ToString();
                digitalSignInfo.Bin = subjectDnValues[subjectDnOids.IndexOf(new DerObjectIdentifier("2.5.4.11"))].ToString().Remove(0, 3);
            }

            var issuerDnValues = certificate.IssuerDN.GetValues();
            var issuerDnOids = certificate.IssuerDN.GetOids();

            digitalSignInfo.Issuer = issuerDnValues[issuerDnOids.IndexOf(new DerObjectIdentifier("2.5.4.3"))].ToString();

            var extendedKeyUsage = certificate.GetExtendedKeyUsage();
            if (extendedKeyUsage != null)
            {
                foreach (var keyUsageOid in extendedKeyUsage)
                {
                    if (KncaOids.UserTypes.TryGetValue(new DerObjectIdentifier((string)keyUsageOid), out string userType))
                    {
                        digitalSignInfo.UserType = userType;
                        break;
                    }
                }

            }

            break;
        }

        return digitalSignInfo;
    }

    /// <summary>
    /// Получить названия подписанных файлов
    /// </summary>
    /// <param name="binaryData"></param>
    /// <returns></returns>
    public string[] GetSignedFileNames(byte[] binaryData)
    {
        CmsSignedData cms = new(binaryData);
        var signers = cms.GetSignerInfos().GetSigners();

        foreach (SignerInformation signerInformation in signers)
        {
            DerObjectIdentifier signedFileNamesOid = new("1.2.840.113549.1.9.77");
            var fileNamesAtr = signerInformation.SignedAttributes[signedFileNamesOid];
            if (fileNamesAtr == null)
                return Array.Empty<string>();
            DerUtf8String value = (DerUtf8String)((DerSet)fileNamesAtr.AttrValues.ToAsn1Object())[0].ToAsn1Object();
            string fileNamesStr = value.GetString();
            string[] fileNames = fileNamesStr.Split("<>");
            return fileNames;
        }
        return Array.Empty<string>();
    }

    /// <summary>
    /// Проверка ЭЦП
    /// </summary>
    /// <param name="signedContent"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public DigitalSignVerifyResultDTO Verify(byte[] signedContent, byte[] sign)
    {
        CmsSignedData cms = new(new CmsProcessableByteArray(signedContent), sign);
        var signers = cms.GetSignerInfos().GetSigners();
        IX509Store certs = cms.GetCertificates("Collection");
        foreach (SignerInformation signerInformation in signers)
        {
            DigitalSignVerifyResultDTO result = new();
            ArrayList certList = new(certs.GetMatches(signerInformation.SignerID));
            X509Certificate certificate = (X509Certificate)certList[0];
            result.CertificateStatus = GetCertificateStatus(certificate);
            try
            {
                bool isValid = signerInformation.Verify(certificate);
                result.IsValid = isValid;
                result.Verified = DateTime.Now;
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Verify");
                result.IsValid = false;
                result.Verified = DateTime.Now;
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        return new DigitalSignVerifyResultDTO();
    }

    /// <summary>
    /// Получить статус сертификата
    /// </summary>
    /// <param name="certificate"></param>
    /// <returns></returns>
    private static CertificateStatusEnum GetCertificateStatus(X509Certificate certificate)
    {
        CertificateStatusEnum status = CertificateStatusEnum.Valid;
        try
        {
            certificate.CheckValidity();
        }
        catch (CertificateExpiredException)
        {
            status = CertificateStatusEnum.Expired;
        }
        catch (CertificateNotYetValidException)
        {
            status = CertificateStatusEnum.NotYetValid;
        }
        // TODO OCSP and CRL
        return status;
    }
}
