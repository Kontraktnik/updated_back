namespace Application.DTO.DigitalSign;

public class CertStatusInfoDTO
{
    public byte[] OcspResponse { get; set; }

    public string OcspInfo { get; set; }

    public string TimeStampInfo { get; set; }

    public byte[] TimeStampResponse { get; set; }

    public bool IsValid { get; set; } = true;

    public bool IsExpired { get; set; }

    public int ExpireDaysCount { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime RevokedDate { get; set; }

    public string RevokedReason { get; set; }

    public int RevokedReasonCode { get; set; }

    public string ErrorInfo { get; set; }

    public string Iin { get; set; }
}
