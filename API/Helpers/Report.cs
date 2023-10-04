using System.Drawing;
using System.Globalization;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.DTO.User;
using Infrastracture.Helpers;
using Ionic.Zip;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace API.Helpers;

public class Report
{
    public async static Task<ResponseDTO<String>> getSurveyInfo(SurveyDTO? surveyDto, UserDTO user, string? fileStoragePath)
    {
        if (surveyDto != null)
        {
            String fileExists = "not file";

            using (var archive = new ZipFile())
            {
                try
                {
                    var docName = fileStoragePath != null ? 
                        Path.Combine(fileStoragePath, $"{AppConstant.UploadsFolder}/{surveyDto.IIN}/anketa_{surveyDto.FullName}_{surveyDto.Id}_{DateTime.Now.ToString("dd_M__yyyy-HH_mm")}.docx") :
                        $"{AppConstant.UploadsFolder}/{surveyDto.IIN}/anketa_{surveyDto.FullName}_{surveyDto.Id}_{DateTime.Now.ToString("dd_M__yyyy-HH_mm")}.docx";

                    using (var document = DocX.Create(docName))
                    {
                        // Save this document to disk.
                        //Title
                        var p_title = document.InsertParagraph();
                        p_title.Append($"Заявка №{surveyDto.Id}")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"))
                            .FontSize(24)
                            .Color(Color.Black)
                            .SpacingAfter(20)
                            .Bold();
                        p_title.Alignment = Alignment.center;
                        //Данные анкеты
                        var p_fullName = document.InsertParagraph();
                        p_fullName.Append($"ФИО в анкете:{surveyDto.FullName} ИИН:{surveyDto.IIN}")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"))
                            .FontSize(14)
                            .SpacingAfter(10)
                            .Color(Color.Black);
                        p_fullName.Alignment = Alignment.left;
                        var p_contact = document.InsertParagraph();
                        p_contact.Append($"Телефон в анкете:{surveyDto.Phone} Почта:{surveyDto.Email}")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"))
                            .FontSize(14)
                            .SpacingAfter(10)
                            .Color(Color.Black);
                        p_contact.Alignment = Alignment.left;
                        var tables = document.AddTable(30, 2);
                        tables.Alignment = Alignment.center;
                        tables.Rows[0].Cells[0].Paragraphs[0].Append("Место проживания")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[0].Cells[1].Paragraphs[0]
                            .Append(surveyDto.BirthArea != null ? surveyDto.BirthArea.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[1].Cells[0].Paragraphs[0].Append("Регион")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[1].Cells[1].Paragraphs[0].Append(surveyDto.Region ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[2].Cells[0].Paragraphs[0].Append("Город/Населенный пункт")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[2].Cells[1].Paragraphs[0].Append(surveyDto.City ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[3].Cells[0].Paragraphs[0].Append("Улица")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[3].Cells[1].Paragraphs[0].Append(surveyDto.Street ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[4].Cells[0].Paragraphs[0].Append("Дом")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[4].Cells[1].Paragraphs[0].Append(surveyDto.Home ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[5].Cells[0].Paragraphs[0].Append("Квартира")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[5].Cells[1].Paragraphs[0].Append(surveyDto.Appartment ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[6].Cells[0].Paragraphs[0].Append("Образование")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[6].Cells[1].Paragraphs[0]
                            .Append(surveyDto.Education != null ? surveyDto.Education.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[7].Cells[0].Paragraphs[0].Append("Опыт работы")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[7].Cells[1].Paragraphs[0]
                            .Append(surveyDto.Experienced == true ? "Имеет опыт работы" : "Не имеет опыта работы")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[8].Cells[0].Paragraphs[0].Append("Прохождение воинской службы")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[8].Cells[1].Paragraphs[0].Append(surveyDto.Served == true ? "Да" : "Нет")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[9].Cells[0].Paragraphs[0].Append("Номер воинской части")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[9].Cells[1].Paragraphs[0].Append(surveyDto.ServedArmyNumber ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[10].Cells[0].Paragraphs[0].Append("Занимаемая должность")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[10].Cells[1].Paragraphs[0].Append(surveyDto.PositionName ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[11].Cells[0].Paragraphs[0].Append("Звание")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[11].Cells[1].Paragraphs[0]
                            .Append(surveyDto.ArmyRank != null ? surveyDto.ArmyRank.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[12].Cells[0].Paragraphs[0].Append("Прохождение обучение в ВТШ МО РК")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[12].Cells[1].Paragraphs[0].Append(surveyDto.VTShServed == true ? "Да" : "Нет")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[13].Cells[0].Paragraphs[0].Append("Наименование филиала ВТШ МО РК")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[13].Cells[1].Paragraphs[0]
                            .Append(surveyDto.VTSh != null ? surveyDto.VTSh.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[14].Cells[0].Paragraphs[0].Append("Год прохождения обучения в филиалах ВТШ МО РК")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[14].Cells[1].Paragraphs[0].Append(surveyDto.VTShYear ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[15].Cells[0].Paragraphs[0].Append("Поступаемая должность")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[15].Cells[1].Paragraphs[0]
                            .Append(surveyDto.Position != null ? surveyDto.Position.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[16].Cells[0].Paragraphs[0].Append("Номер воинской части")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[16].Cells[1].Paragraphs[0].Append(surveyDto.ArmyNumber ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[17].Cells[0].Paragraphs[0].Append("Срок заключения контракта")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[17].Cells[1].Paragraphs[0].Append(surveyDto.ContractYear.ToString() ?? "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[18].Cells[0].Paragraphs[0].Append("Область")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[18].Cells[1].Paragraphs[0]
                            .Append(surveyDto.Area != null ? surveyDto.Area.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[19].Cells[0].Paragraphs[0].Append("Вакансия")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[19].Cells[1].Paragraphs[0]
                            .Append(surveyDto.Vacancy != null ? surveyDto.Vacancy.Position.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[20].Cells[0].Paragraphs[0].Append("Текущий этап")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[20].Cells[1].Paragraphs[0]
                            .Append(surveyDto.StepGroup != null ? surveyDto.StepGroup.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[21].Cells[0].Paragraphs[0].Append("Текущий этап")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[21].Cells[1].Paragraphs[0]
                            .Append(surveyDto.CurrentStep != null ? surveyDto.CurrentStep.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[22].Cells[0].Paragraphs[0].Append("Статус")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[22].Cells[1].Paragraphs[0]
                            .Append(surveyDto.Status == 1
                                ? "Пройден"
                                : (surveyDto.Status == 0 ? "В работе" : "Отказано"))
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[23].Cells[0].Paragraphs[0].Append("Медицинский статус")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[23].Cells[1].Paragraphs[0]
                            .Append(surveyDto.MedicalStatus != null ? surveyDto.MedicalStatus.TitleRu : "Не указано")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[24].Cells[0].Paragraphs[0].Append("Дата создание заявки")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[24].Cells[1].Paragraphs[0]
                            .Append(surveyDto.CreatedAt != null
                                ? surveyDto.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss")
                                : "Не указано").Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[25].Cells[0].Paragraphs[0].Append("Дата последнего обновление заявки")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[25].Cells[1].Paragraphs[0]
                            .Append(surveyDto.UpdatedAt != null
                                ? surveyDto.UpdatedAt?.ToString("dd/MM/yyyy hh:mm:ss")
                                : "Не указано").Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[26].Cells[0].Paragraphs[0].Append("ФИО запрашивающего")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[26].Cells[1].Paragraphs[0].Append(user.FullName)
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[27].Cells[0].Paragraphs[0].Append("ИИН запрашивающего")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[27].Cells[1].Paragraphs[0].Append(user.IIN)
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));

                        tables.Rows[28].Cells[0].Paragraphs[0].Append("Дата формирования отчета")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        tables.Rows[28].Cells[1].Paragraphs[0].Append(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"))
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));



                        var r = tables.InsertRow();
                        var p = document.InsertParagraph("Таблица 1 - Указанные данные:")
                            .Font(new Xceed.Document.NET.Font("Times New Roman"));
                        p.InsertTableAfterSelf(tables);
                        if (surveyDto.SurveyRelatives != null && surveyDto.SurveyRelatives.Count > 0)
                        {
                            var relative_tables = document.AddTable(surveyDto.SurveyRelatives.Count + 1, 5);
                            int count = 1;
                            relative_tables.Rows[0].Cells[0].Paragraphs[0].Append("Фамилия")
                                .Font(new Xceed.Document.NET.Font("Times New Roman")).Bold();
                            relative_tables.Rows[0].Cells[1].Paragraphs[0].Append("Имя")
                                .Font(new Xceed.Document.NET.Font("Times New Roman")).Bold();
                            relative_tables.Rows[0].Cells[2].Paragraphs[0].Append("Отчество")
                                .Font(new Xceed.Document.NET.Font("Times New Roman")).Bold();
                            relative_tables.Rows[0].Cells[3].Paragraphs[0].Append("ИИН")
                                .Font(new Xceed.Document.NET.Font("Times New Roman")).Bold();
                            relative_tables.Rows[0].Cells[4].Paragraphs[0].Append("Дата рождения")
                                .Font(new Xceed.Document.NET.Font("Times New Roman")).Bold();
                            foreach (var relative in surveyDto.SurveyRelatives)
                            {
                                relative_tables.Rows[count].Cells[0].Paragraphs[0]
                                    .Append(relative.SurName ?? "Не указано")
                                    .Font(new Xceed.Document.NET.Font("Times New Roman"));
                                relative_tables.Rows[count].Cells[1].Paragraphs[0].Append(relative.Name ?? "Не указано")
                                    .Font(new Xceed.Document.NET.Font("Times New Roman"));
                                relative_tables.Rows[count].Cells[2].Paragraphs[0]
                                    .Append(relative.Patronomic ?? "Не указано")
                                    .Font(new Xceed.Document.NET.Font("Times New Roman"));
                                relative_tables.Rows[count].Cells[3].Paragraphs[0].Append(relative.IIN ?? "Не указано")
                                    .Font(new Xceed.Document.NET.Font("Times New Roman"));
                                relative_tables.Rows[count].Cells[4].Paragraphs[0].Append(
                                    relative.BirthDate != null && relative.BirthDate.Length == 8
                                        ? DateTime.ParseExact(relative.BirthDate, "ddMMyyyy",
                                            CultureInfo.InvariantCulture).ToString("dd.MM.yyyy")
                                        : "Не указано").Font(new Xceed.Document.NET.Font("Times New Roman"));
                                count++;
                            }

                            var p_relatives = document.InsertParagraph("Таблица 2 - Указанные родственники:").SpacingBefore(10)
                                .Font(new Xceed.Document.NET.Font("Times New Roman"));
                            p_relatives.InsertTableAfterSelf(relative_tables);
                        }
                        document.Save();
                        ZipEntry zipFile = archive.AddFile(docName);
                        zipFile.FileName = $"anketa_{surveyDto.FullName}_{surveyDto.Id}_{DateTime.Now.ToString("dd_M__yyyy-HH_mm")}.docx";


                    }
                }
                catch (Exception ex)
                {

                }


                //Add Autobiography
                if (System.IO.File.Exists(surveyDto.AutoBiography))
                {
                    var filename = System.IO.Path.GetExtension(surveyDto.AutoBiography);
                    ZipEntry zipFile = archive.AddFile(surveyDto.AutoBiography);
                    zipFile.FileName = $"autobiography_{surveyDto.FullName}_{surveyDto.Id}{filename}";
                }
                //Add Photo
                if (System.IO.File.Exists(surveyDto.ImageUrl))
                {
                    var filename = System.IO.Path.GetExtension(surveyDto.ImageUrl);
                    ZipEntry zipFile = archive.AddFile(surveyDto.ImageUrl);
                    zipFile.FileName = $"photo_{surveyDto.FullName}_{surveyDto.Id}{filename}";
                }
                //Add Education
                if (System.IO.File.Exists(surveyDto.EducationUrl))
                {
                    var filename = System.IO.Path.GetExtension(surveyDto.EducationUrl);
                    ZipEntry zipFile = archive.AddFile(surveyDto.EducationUrl);
                    zipFile.FileName = $"education_{surveyDto.FullName}_{surveyDto.Id}{filename}";

                }
                //Add IncomeUrl
                if (System.IO.File.Exists(surveyDto.IncomePropertyUrl))
                {
                    var filename = System.IO.Path.GetExtension(surveyDto.IncomePropertyUrl);
                    ZipEntry zipFile = archive.AddFile(surveyDto.IncomePropertyUrl);
                    zipFile.FileName = $"income_{surveyDto.FullName}_{surveyDto.Id}{filename}";

                }
                //Add Employement
                if (System.IO.File.Exists(surveyDto.EmploymentUrl))
                {
                    var filename = System.IO.Path.GetExtension(surveyDto.EmploymentUrl);
                    ZipEntry zipFile = archive.AddFile(surveyDto.EmploymentUrl);
                    zipFile.FileName = $"labor_{surveyDto.FullName}_{surveyDto.Id}{filename}";
                }
                //Add Military Url
                if (System.IO.File.Exists(surveyDto.MillitaryUrl))
                {
                    var filename = System.IO.Path.GetExtension(surveyDto.MillitaryUrl);
                    ZipEntry zipFile = archive.AddFile(surveyDto.MillitaryUrl);
                    zipFile.FileName = $"militaty_{surveyDto.FullName}_{surveyDto.Id}{filename}";

                }
                //Add SpecialCheckUrl
                if (System.IO.File.Exists(surveyDto.SpecialCheckUrl))
                {
                    var filename = System.IO.Path.GetExtension(surveyDto.SpecialCheckUrl);
                    ZipEntry zipFile = archive.AddFile(surveyDto.SpecialCheckUrl);
                    zipFile.FileName = $"anketa_{surveyDto.FullName}_{surveyDto.Id}{filename}";
                }

                fileExists = fileStoragePath != null
                    ?
                    Path.Combine(fileStoragePath, $"{AppConstant.UploadsFolder}/{surveyDto.IIN}/result_{surveyDto.FullName}_{surveyDto.Id}_{DateTime.Now.ToString("dd_M__yyyy-HH_mm")}.zip")
                    :
                    $"{AppConstant.UploadsFolder}/{surveyDto.IIN}/result_{surveyDto.FullName}_{surveyDto.Id}_{DateTime.Now.ToString("dd_M__yyyy-HH_mm")}.zip";
                archive.Save(fileExists);
            }

            return new ResponseDTO<String>()
            {
                Success = true,
                StatusCode = 200,
                Data = fileExists
            };
        }
        else
        {
            return new ResponseDTO<String>()
            {
                Success = false,
                StatusCode = 404
            };
        }
    }
}