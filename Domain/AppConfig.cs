namespace Domain;

public class AppConfig
{
    public string? FileStoragePath { get; set; }
    public string? UploadRequestStoragePath { get; set; }
    public string? BackupRequestStoragePath { get; set; }
    public string DbType { get; set; }
    public bool UseHttpsRedirection { get; set; }
    public bool UseSpaServices { get; set; }
    public string[] Origins { get; set; }
    public string[] DevOrigins { get; set; }

    public string? SMSLogin { get; set; }

    public string? SMSPassword { get; set; }
}
