using System.Security.Claims;
using API.Helpers;
using Domain;
using Google.Protobuf.WellKnownTypes;
using Infrastracture.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.FileController;

public class FileController : BaseApiController
{
    private readonly AppConfig _config;
    private string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".csv", ".rtf", ".pdf" };
    public FileController(AppConfig config)
    {
        _config = config;
    }

    [DisableRequestSizeLimit]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<string>> uploadFile([FromForm] IFormFile formFile)
    {
        
        var iin = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (formFile != null)
        {
            var extension = Path.GetExtension(formFile.FileName);
            if(!Array.Exists(this.allowedExtensions, ext => ext == extension.ToLower()))
            {
                return BadRequest();
            }
            var new_name = Path.GetFileNameWithoutExtension(formFile.FileName);
            var path = _config.FileStoragePath != null ? Path.Combine(_config.FileStoragePath, $"{AppConstant.UploadsFolder}/{iin}") : $"{AppConstant.UploadsFolder}/{iin}";
            var filePath = Path.Combine(path,$"{new_name}_{Guid.NewGuid()}{extension}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return Ok(filePath);
        }
        else
        {
            return NotFound();
        }
    } 
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [DisableRequestSizeLimit]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<List<string>>> BrowseUserFile([FromBody] string IIN)
    {
        var path = Path.Combine(_config.FileStoragePath, $"{AppConstant.UploadsFolder}/{IIN}");
        var allFiles = new List<string>();
        allFiles.Add(path);

        if (Directory.Exists(path))
        {
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                
                allFiles.Add(file);
            }

            return allFiles;
        }
        else
        {
            return allFiles;
        }
    }


    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [DisableRequestSizeLimit]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<bool>> DeleteFile([FromBody] string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    public ActionResult DownloadFile([FromBody] string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/force-download", filePath);  
        }

        return NotFound();
    }

}