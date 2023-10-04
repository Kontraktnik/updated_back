using Application.Contracts.Service;
using Application.DTO.Common;
using Application.DTO.DigitalSign;
using Application.Features.DigitalSignCQRS.Query.GetDigitalSignBinaryById;
using Application.Features.DigitalSignCQRS.Query.GetDigitalSigns;
using Infrastracture.Contracts.Specifications.DigitalSignSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System.Security.Claims;

namespace API.Controllers.DigitalSignController;

public class DigitalSignController : BaseApiController
{
    private readonly ICryptoService _cryptoService;
    private readonly IDigitalSignService _digitalSignService;
    private readonly ILogger<DigitalSignController> _logger;
    private readonly IMediator _mediator;

    public DigitalSignController(ICryptoService cryptoService,
        IDigitalSignService digitalSignService,
        ILogger<DigitalSignController> logger,
        IMediator mediator)
    {
        _cryptoService = cryptoService;
        _digitalSignService = digitalSignService;
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Проверка сертификата
    /// </summary>
    /// <param name="validateRequest"></param>
    /// <returns></returns>
    [HttpPost("ValidateCertificate")]
    public async Task<ActionResult<ResponseDTO<CertStatusInfoDTO>>> ValidateCertificate([FromBody] ValidateRequestDTO validateRequest)
    {
        byte[] certificateBinary = Convert.FromBase64String(validateRequest.CertificateBase64);
        CertStatusInfoDTO result = await _cryptoService.ValidateCertificate(certificateBinary);

        return Ok(new ResponseDTO<CertStatusInfoDTO>(result));
    }

    /// <summary>
    /// Получить данные для подписи
    /// </summary>
    /// <param name="surveyId">ИД заявки</param>
    /// <returns></returns>
    [HttpGet("GetSigningData/{surveyId}")]
    public async Task<ActionResult<ResponseDTO<SigningDataDTO?>>> GetSigningData(long surveyId)
    {
        SigningDataDTO? result = await _digitalSignService.GetSigningData(surveyId, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return Ok(new ResponseDTO<SigningDataDTO?>(result));
    }

    /// <summary>
    /// Сохранение ЭЦП в формате Base64
    /// </summary>
    /// <param name="sign">ЭЦП в формате Base64</param>
    /// <param name="data">данные DigitalSignInputDTO</param>
    /// <returns>ИД сохраненной записи</returns>
    [HttpPost("UploadSignBase64")]
    public async Task<ActionResult<ResponseDTO<long?>>> UploadSignBase64([FromForm] string sign, [FromForm] string data)
    {
        try
        {
            _logger.LogInformation($"SIGN INPUT DATA: {data}");

            var inputDto = JsonConvert.DeserializeObject<DigitalSignInputDTO>(data);

            long? result = await _digitalSignService.SaveSignBase64(inputDto, Convert.FromBase64String(sign), User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(new ResponseDTO<long?>(result));
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR: {ex.Message}; InnerException: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}; StackTrace: {ex.StackTrace}");
            return Ok(new ResponseDTO<long?>(ex.Message));
        }
    }

    /// <summary>
    /// Получить все ЭЦП заявки
    /// </summary>
    /// <param name="surveyId">ИД заявки</param>
    /// <returns></returns>
    [HttpGet("GetDigitalSigns/{surveyId}")]
    public async Task<ActionResult<ResponseDTO<ICollection<DigitalSignDTO>>>> GetDigitalSigns(long surveyId)
    {
        var result = await _mediator.Send(new GetDigitalSigns(new DigitalSignSpecification(surveyId)));

        return Ok(result);
    }

    /// <summary>
    /// Проверка ЭЦП
    /// </summary>
    /// <param name="surveyId"></param>
    /// <returns></returns>
    [HttpGet("VerifyDigitalSigns/{surveyId}")]
    public async Task<ActionResult<ResponseDTO<ICollection<DigitalSignVerifyResultDTO>>>> VerifyDigitalSigns(long surveyId)
    {
        var digitalSigns = await _mediator.Send(new GetDigitalSigns(new DigitalSignSpecification(surveyId)));

        List<DigitalSignVerifyResultDTO> results = new();
        foreach (var digitalSign in digitalSigns.Data)
        {
            var digitalSignBinary = await _mediator.Send(new GetDigitalSignBinaryById(new DigitalSignBinarySpecification(digitalSign.BinaryDataId)));
            string[] fileNames = _cryptoService.GetSignedFileNames(digitalSignBinary.Data);

            var signData = await _digitalSignService.GetSigningData(surveyId, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, fileNames);

            var result = _cryptoService.Verify(signData.MessageDigest, digitalSignBinary.Data);
            if (!result.IsValid)
            {
                result.SledBase64 = Convert.ToBase64String(signData.MessageDigest);
                result.SignBase64 = Convert.ToBase64String(digitalSignBinary.Data);
                result.DigitalSignId = digitalSign.BinaryDataId;
            }

            results.Add(result);
        }

        return Ok(new ResponseDTO<ICollection<DigitalSignVerifyResultDTO>>(results));
    }
}
