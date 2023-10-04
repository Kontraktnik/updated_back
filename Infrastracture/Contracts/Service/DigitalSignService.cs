using Application.Contracts.Service;
using Application.DTO.DigitalSign;
using Application.DTO.Survey;
using Application.Features.DigitalSignCQRS.Command.AddDigitalSign;
using Application.Features.DigitalSignCQRS.Query.GetDigitalSignAttributes;
using Application.Features.SurveyCQRS.Query.GetSurveyWithSpecAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Infrastracture.Contracts.Specifications.DigitalSignSpecification;
using Infrastracture.Contracts.Specifications.SurveySpecification;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infrastracture.Contracts.Service;

public class DigitalSignService : IDigitalSignService
{
    private readonly ILogger<DigitalSignService> _logger;
    private readonly IMediator _mediator;
    private readonly ICryptoService _cryptoService;

    public DigitalSignService(ILogger<DigitalSignService> logger,
        IMediator mediator,
        ICryptoService cryptoService)
    {
        _logger = logger;
        _mediator = mediator;
        _cryptoService = cryptoService;
    }

    /// <summary>
    /// Получить данные для подписи
    /// </summary>
    /// <param name="surveyId">Идентификатор заявки</param>
    /// <param name="iin">ИИН заявителя</param>
    /// <param name="fileNames">Названия подписанных файлов</param>
    /// <returns></returns>
    public async Task<SigningDataDTO?> GetSigningData(long surveyId, string? iin, string[]? fileNames = null)
    {
        try
        {
            if (!string.IsNullOrEmpty(iin))
            {
                var user = await _mediator.Send(new GetUserByIdAsync(new UserSpecification(iin, null)));
                var signAttributes = await _mediator.Send(new GetDigitalSignAttributes(new DigitalSignAttributeSpecification()));

                SurveySpecification specification = new(surveyId, user.Data);
                var survey = await _mediator.Send(new GetSurveyWithSpecAsyncQuery(specification, user.Data));

                if (survey != null)
                {
                    SigningDataDTO result = new();

                    Stream stream = new MemoryStream();
                    foreach (var attribute in signAttributes.Data.Where(d => d.AttributeType == AppConstant.DigitalSignAttributeField).OrderBy(x => x.Order))
                    {
                        string? stringValue = GetFieldValue(attribute, survey.Data);
                        if (!string.IsNullOrEmpty(stringValue))
                        {
                            await WriteToStreamAsync(stream, attribute, Encoding.UTF8.GetBytes(stringValue ?? ""));

                            SigningAttributeDTO attributeDto = new()
                            {
                                DisplayName = attribute.DisplayName,
                                Value = stringValue
                            };
                            result.Attributes.Add(attributeDto);
                        }
                    }

                    foreach (var attribute in signAttributes.Data.Where(d => d.AttributeType == AppConstant.DigitalSignAttributeFile).OrderBy(x => x.Order))
                    {
                        var filePath = GetFilePath(attribute, survey.Data);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            try
                            {
                                string fileName = FileHelper.GetFileName(filePath);
                                if (fileNames != null && fileNames.Contains(fileName) || fileNames == null)
                                {
                                    var fileByte = await File.ReadAllBytesAsync(filePath);

                                    await WriteToStreamAsync(stream, attribute, fileByte);

                                    AttachedFileDTO attachedFileDto = new()
                                    {
                                        FileUrl = filePath,
                                        Name = fileName,
                                        Extention = FileHelper.GetFileExtention(filePath),
                                        FileSize = FileHelper.GetFileSize(filePath)
                                    };
                                    result.Attachments.Add(attachedFileDto);
                                }
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(1, e, "GetSigningData File.ReadAllBytesAsync");
                            }
                        }
                    }

                    stream.Seek(0, SeekOrigin.Begin);
                    var sledByte = FileHelper.ReadFully(stream);

                    result.MessageDigest = _cryptoService.GetMessageDigest(sledByte);

                    return result;
                }
                else
                {
                    _logger.LogWarning($"GetSigningData Заявка по идентификатору '{surveyId}' не найдена");
                    return null;
                }
            }
            else
            {
                _logger.LogWarning("GetSigningData Отсутствует ИИН");
                return null;
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(1, ex, "GetSigningData");
            return null;
        }
    }

    /// <summary>
    /// Сохранить подписанные данные
    /// </summary>
    /// <param name="info">Инфо о подписи</param>
    /// <param name="binaryData">Подписанные данные</param>
    /// <param name="iin">ИИН текущего пользователя</param>
    /// <returns></returns>
    public async Task<long?> SaveSignBase64(DigitalSignInputDTO? info, byte[] binaryData, string? iin)
    {
        try
        {
            var user = await _mediator.Send(new GetUserByIdAsync(new UserSpecification(iin, null)));

            DigitalSignInfoDTO infoDto = _cryptoService.GetDigitalSignInfo(binaryData);

            DigitalSignBaseDTO digitalSign = new()
            {
                FileName = info?.FileName,
                WhoSignedId = user.Data.Id,
                Signed = info?.Signed ?? DateTime.Now,
                SurveyId = info?.SurveyId,
            };

            var result = await _mediator.Send(new AddDigitalSignCommand(digitalSign, binaryData, infoDto));

            return result.Data;
        }
        catch(Exception ex)
        {
            _logger.LogError(1, ex, "SaveSignBase64");

            return null;
        }
    }

    /// <summary>
    /// Получить значение поля для добавления в МЭД
    /// </summary>
    /// <param name="attribute"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetFieldValue(DigitalSignAttributeDTO attribute, SurveyDTO survey)
    {
        return attribute.FieldName switch
        {
            "IIN" => survey.IIN,
            "FullName" => survey.FullName,
            "Email" => survey.Email,
            "Phone" => survey.Phone,
            "BirthArea" => survey.BirthArea?.TitleRu,
            "Region" => survey.Region,
            "City" => survey.City,
            "Street" => survey.Street,
            "Home" => survey.Home,
            "Appartment" => survey.Appartment,
            "Education" => survey.Education?.TitleRu,
            "ServedArmyNumber" => survey.ServedArmyNumber,
            "PositionName" => survey.PositionName,
            "ArmyRank" => survey.ArmyRank?.TitleRu,
            "VTSh" => survey.VTSh?.TitleRu,
            "VTShYear" => survey.VTShYear,
            "Position" => survey.Position?.TitleRu,
            "ArmyNumber" => survey.ArmyNumber,
            "ContractYear" => survey.ContractYear.ToString(),
            "Area" => survey.Area?.TitleRu,
            "Vacancy" => survey.Vacancy?.DescriptionRu,
            "CreatedAt" => survey.Vacancy?.CreatedAt.ToString("dd.MM.yyyy HH:mm:ss"),
            _ => null
        };
    }

    /// <summary>
    /// Получить путь файлу согласно атрибуту
    /// </summary>
    /// <param name="attribute">Атрибут</param>
    /// <param name="survey">Заявка</param>
    /// <returns></returns>
    private static string? GetFilePath(DigitalSignAttributeDTO attribute, SurveyDTO survey)
    {
        return attribute.FieldName switch
        {
            "ImageUrl" => survey.ImageUrl,
            "AutoBiography" => survey.AutoBiography,
            "EducationUrl" => survey.EducationUrl,
            "IncomePropertyUrl" => survey.IncomePropertyUrl,
            "EmploymentUrl" => survey.EmploymentUrl,
            "MillitaryUrl" => survey.MillitaryUrl,
            "SpecialCheckUrl" => survey.SpecialCheckUrl,
            "IdentityCardUrl" => survey.IdentityCardUrl,
            "TuberUrl" => survey.TuberUrl,
            "DermatologUrl" => survey.DermatologUrl,
            "PsychoNeurologicalUrl" => survey.PsychoNeurologicalUrl,
            "NarcologicalUrl" => survey.NarcologicalUrl,
            _ => null
        };
    }

    /// <summary>
    /// Записать данные в поток
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <param name="attribute">Атрибут</param>
    /// <param name="data">Данные</param>
    /// <returns></returns>
    private static async Task WriteToStreamAsync(Stream stream, DigitalSignAttributeDTO attribute, byte[] data)
    {
        if (!string.IsNullOrEmpty(attribute.Prefix))
        {
            await stream.WriteAsync(Encoding.UTF8.GetBytes(attribute.Prefix).AsMemory(0, attribute.Prefix.Length));
        }

        await stream.WriteAsync(data.AsMemory(0, data.Length));
        await stream.WriteAsync(Encoding.UTF8.GetBytes(AppConstant.DigitalSignEnding).AsMemory(0, AppConstant.DigitalSignEnding.Length));
    }

}
