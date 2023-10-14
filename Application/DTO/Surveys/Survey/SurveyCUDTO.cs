namespace Application.DTO.Survey;

public class SurveyCUDTO
{
    public string IIN { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    
    public long BirthAreaId { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string? Home { get; set; }
    public string? Appartment { get; set; }
    
    public long EducationId { get; set; }
    public bool Experienced { get; set; }
    
    public bool Served { get; set; }
    public string? ServedArmyNumber { get; set; }
    public string? PositionName { get; set; }
    public long? ArmyRankId { get; set; }
    
    public bool VTShServed { get; set; }
    public long? VTShId { get; set; }
    public string? VTShYear { get; set; }

    public long PositionId { get; set; }
    public string? ArmyNumber { get; set; }
    public int ContractYear { get; set; }
    
    
    public long AreaId { get; set; }
    public long? VacancyId {get; set;}
    
    public List<long>? DriverLicense { get; set; }
    public List<SurveyRelativeCUDTO>? Relatives { get; set; }

    //Документы
    public string ImageUrl { get; set; }
    public string AutoBiography { get; set; }
    public string EducationUrl { get; set; }
    public string IncomePropertyUrl { get; set; }
    public string EmploymentUrl { get; set; }
    public string MillitaryUrl { get; set; }
    public string SpecialCheckUrl { get; set; }
    public string IdentityCardUrl { get; set; }
    
    
    public bool Agreed { get; set; }
    public string SignKey { get; set; }
    public string? UserSign { get; set; }
}