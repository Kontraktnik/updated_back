namespace Application.DTO.DigitalSign;

public class CryptoConf
{
    public string TimeStampUrl { get; set; }
    public string OcspUrl { get; set; }
    public string? UCGORootCertificatePath { get; set; }
}
