using API.Helpers;
using Application.DTO.FileDTO;
using Domain;
using Infrastracture.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API.Controllers;

public class BackUpController : BaseApiController
{
    private readonly string _connectionString;
    private readonly AppConfig _config;

    public BackUpController(IConfiguration configuration, AppConfig config)
    {
        _connectionString = configuration.GetConnectionString("MySqlConnectionString");
        _config = config;
    }

    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpGet]
    public async Task<ActionResult<string>> GenerateBackupFile([FromQuery] List<string>? tables = null)
    {
        try
        {
            string backup = "backup_";
            if (tables != null || tables.Count > 0)
            {
                foreach (var item in tables)
                {
                    backup += $"_{item}_";
                }
            }
            else
            {
                backup += "_all_";
            }

            var filePath = Path.Combine(_config.FileStoragePath, "backups",
                $"{backup}{DateTime.UtcNow.Day}_{DateTime.UtcNow.Month}_{DateTime.UtcNow.Year}-{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.sql");

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        if (tables != null || tables.Count > 0)
                        {
                            mb.ExportInfo.TablesToBeExportedList = tables;
                        }

                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(filePath);
                        conn.Close();
                    }
                }
            }

            return Ok(filePath);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }



    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    public async Task<bool> ImportBackUpFile(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
        {
            return false;
        }
        using (MySqlConnection conn = new MySqlConnection(_connectionString))
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    mb.ImportFromFile(filePath);
                    conn.Close();
                }
            }
        }

        return true;
    }

    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpGet]
    public async Task<List<BackupDTO>> GetAllBackups()
    {
        var backups = new List<BackupDTO>();
        string[] fileArrays = Directory.GetFiles(Path.Combine(_config.FileStoragePath, "backups"));
        if (fileArrays.Length > 0)
        {
            foreach (var fileItem in fileArrays)
            {
                if (System.IO.File.Exists(fileItem))
                {
                    FileInfo info = new FileInfo(fileItem);
                    backups.Add(new BackupDTO()
                    {
                        FullPath = fileItem,
                        FileName = info.Name,
                        FileSize = info.Length / 1024,
                        Created = info.CreationTime,
                        Extension = info.Extension
                    });
                }
            }
        }
        return backups;
    }

    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpGet]
    public async Task<IActionResult> DownloadBackups(string filePath)
    {

        if (System.IO.File.Exists(filePath))
        {
            return File(System.IO.File.OpenRead(filePath), "text/plain", Path.GetFileName(filePath));
        }
        return BadRequest();
    }

    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpGet]
    public async Task<bool> DeleteBackup(string filePath)
    {

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return true;
        }
        return false;
    }

}