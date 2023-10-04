using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Profile;
using Application.DTO.Step;
using Application.DTO.System;
using Application.DTO.User;
using Application.DTO.Vacancy;

namespace Application.DTO.Survey;

public class SurveyDTO
{
    public long Id { get; set; }
     //Данные пользователя
    public long UserId { get; set; }
    public virtual UserDTO User { get; set; }
    
    public string IIN { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    //Место Жительства
    public long BirthAreaId { get; set; }
    public virtual AreaDTO BirthArea { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string? Home { get; set; }
    public string? Appartment { get; set; }
    //Образование
    public long EducationId { get; set; }
    public virtual EducationDTO Education { get; set; }
    public bool Experienced { get; set; }
    
    public bool Served { get; set; }
    public string? ServedArmyNumber { get; set; }
    public string? PositionName { get; set; }
    public long? ArmyRankId { get; set; }
    public virtual ArmyRankDTO? ArmyRank { get; set; }
    
    public bool VTShServed { get; set; }
    public long? VTShId { get; set; }
    public virtual VTShDTO? VTSh { get; set; }
    public string? VTShYear { get; set; }
    
    public long PositionId { get; set; }
    public PositionRDTO Position { get; set; }
    public string? ArmyNumber { get; set; }
    public int ContractYear { get; set; }
    
    public long AreaId { get; set; }
    public virtual AreaDTO Area { get; set; }
    public long? VacancyId { get; set; }
    public virtual VacancyRDTO? Vacancy { get; set; }
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
    
    public long? StepGroupId { get; set; }
    public virtual StepGroupDTO? StepGroup { get; set; }
    
    public long? CurrentStepId { get; set; }
    public virtual StepDTO? CurrentStep { get; set; }
    public int Status { get; set; }
    
    public long? MedicalStatusId { get; set; }
    public virtual MedicalStatusDTO? MedicalStatus { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<SurveyDriverDTO> SurveyDrivers;
    public  ICollection<SurveyRelativeDTO> SurveyRelatives;
    public  SurveyExecutorDTO SurveyExecutor;

    public  ICollection<ProfileDTO> Profiles { get; set; }

}