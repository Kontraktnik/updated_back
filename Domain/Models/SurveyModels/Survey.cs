using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Models.CalculationModels;
using Domain.Models.DigitalSignModels;
using Domain.Models.ProfileModels;
using Domain.Models.StepModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Domain.Models.VacancyModel;

namespace Domain.Models.SurveyModels;

public class Survey : BaseModel
{
    //Данные пользователя
    public long UserId { get; set; }
    public virtual User User { get; set; }
    
    public string IIN { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    //Место Жительства
    [ForeignKey("BirthArea")]
    public long BirthAreaId { get; set; }
    public virtual Area BirthArea { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string? Home { get; set; }
    public string? Appartment { get; set; }
    //Образование
    public long EducationId { get; set; }
    public virtual Education Education { get; set; }
    public bool Experienced { get; set; }
    
    public bool Served { get; set; }
    public string? ServedArmyNumber { get; set; }
    public string? PositionName { get; set; }
    [ForeignKey("Army")]
    public long? ArmyRankId { get; set; }
    public virtual ArmyRank? ArmyRank { get; set; }
    
    public bool VTShServed { get; set; }
    [ForeignKey("VTSh")]
    public long? VTShId { get; set; }
    public virtual VTSh? VTSh { get; set; }
    public string? VTShYear { get; set; }
    
    [ForeignKey("Position")]
    public long PositionId { get; set; }
    public Position Position { get; set; }
    public string? ArmyNumber { get; set; }
    public int ContractYear { get; set; }
    
    [ForeignKey("Area")]
    public long AreaId { get; set; }
    public virtual Area Area { get; set; }
    [ForeignKey("Vacancy")]
    public long? VacancyId { get; set; }
    public virtual Vacancy? Vacancy { get; set; }
    //Документы
    public string ImageUrl { get; set; }
    public string AutoBiography { get; set; }
    public string EducationUrl { get; set; }
    public string IncomePropertyUrl { get; set; }
    public string EmploymentUrl { get; set; }
    public string MillitaryUrl { get; set; }
    public string SpecialCheckUrl { get; set; }
    public string IdentityCardUrl { get; set; }
    //Мед Документы
    public string? TuberUrl { get; set; }
    public string? DermatologUrl { get; set; }
    public string? PsychoNeurologicalUrl { get; set; }
    public string? NarcologicalUrl { get; set; }
    
    public bool Agreed { get; set; }
    public string SignKey { get; set; }
    
    [ForeignKey("StepGroup")]
    public long? StepGroupId { get; set; }
    public virtual StepGroup? StepGroup { get; set; }
    
    [ForeignKey("Step")]
    public long? CurrentStepId { get; set; }
    public virtual Step? CurrentStep { get; set; }
    public int Status { get; set; }
    
    [ForeignKey("MedicalStatus")]
    public long? MedicalStatusId { get; set; }
    public virtual MedicalStatus? MedicalStatus { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public virtual ICollection<SurveyDriver> SurveyDrivers { get; set; }
    public virtual ICollection<SurveyRelative> SurveyRelatives { get; set; }
    
    public virtual SurveyExecutor SurveyExecutor { get; set; }
    
    public virtual ICollection<Profile> Profiles { get; set; }


    [JsonIgnore]
    public virtual ICollection<DigitalSign> DigitalSigns { get; set; }
}