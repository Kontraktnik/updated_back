using Application.DTO.System;
using Application.DTO.Auth;
using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Profile;
using Application.DTO.Step;
using Application.DTO.Survey;
using Application.DTO.Surveys.SurveyStatistics;
using Application.DTO.User;
using Application.DTO.Vacancy;
using Domain.Models.CalculationModels;
using Domain.Models.ProfileModels;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Domain.Models.VacancyModel;
using Profile = AutoMapper.Profile;
using SurveyDriver = Domain.Models.SurveyModels.SurveyDriver;

namespace Application.Mapping;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //System
        CreateMap<Area, AreaDTO>().ReverseMap();
        CreateMap<ArmyDepartment, ArmyDepartmentDTO>().ReverseMap();
        CreateMap<ArmyRank, ArmyRankDTO>().ReverseMap();
        CreateMap<ArmyRegion, ArmyRegionDTO>().ReverseMap();
        CreateMap<ArmyType, ArmyTypeDTO>().ReverseMap();
        CreateMap<DriverLicense, DriverLicenseDTO>().ReverseMap();
        CreateMap<VTSh, VTShDTO>().ReverseMap();
        CreateMap<Education, EducationDTO>().ReverseMap();
        CreateMap<Relative, RelativeDTO>().ReverseMap();
        CreateMap<MedicalStatus, MedicalStatusDTO>().ReverseMap();
        //Calculation
        CreateMap<CategoryPosition, CategoryPositionDTO>().ReverseMap();
        CreateMap<JobCategory, JobCategoryDTO>().ReverseMap();
        CreateMap<Qualification, QualificationDTO>().ReverseMap();
        CreateMap<ServiceYear, ServiceYearDTO>().ReverseMap();
        CreateMap<SecretLevel, SecretLevelDTO>().ReverseMap();
         //RankSalary
         CreateMap<RankSalary, RankSalaryRDTO>().ReverseMap();
         CreateMap<RankSalary, RankSalaryCUDTO>().ReverseMap();
         //Position
         CreateMap<Position, PositionRDTO>().ReverseMap();
         CreateMap<Position, PositionCUDTO>().ReverseMap();
         //JobYear
         CreateMap<JobYear, JobYearRDTO>().ReverseMap();
         CreateMap<JobYear, JobYearCUDTO>().ReverseMap();
         //Step
         CreateMap<StepGroup, StepGroupDTO>().ReverseMap();
         CreateMap<Step, StepDTO>().ReverseMap();
         CreateMap<Step, StepUpdateDTO>().ReverseMap();
         CreateMap<StepOrder, StepOrderDTO>().ReverseMap();
         CreateMap<Role, RoleDTO>();
            
         //Vacancy
         CreateMap<Vacancy, VacancyRDTO>().ReverseMap();
         CreateMap<Vacancy, VacancyCUDTO>().ReverseMap();

         CreateMap<RegisterDTO, User>();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserCreateDTO>().ReverseMap();
        CreateMap<User, UserUpdateDTO>().ReverseMap();
        
        //Surveys
        CreateMap<Survey, SurveyDTO>().ReverseMap();
        CreateMap<Survey, SurveyCUDTO>().ReverseMap();
        //SurveyStatistics
        CreateMap<SurveyStatistics, SurveyStatisticsDTO>().ReverseMap();
        //SurveyExecutor
        CreateMap<SurveyExecutor, SurveyExecutorDTO>().ReverseMap();
        CreateMap<SurveyExecutor, SurveyExecutorCUDTO>().ReverseMap();
        //SurveyDriver
        CreateMap<SurveyDriver, SurveyDriverDTO>().ReverseMap();
        //SurveyRelative
        CreateMap<SurveyRelative, SurveyRelativeDTO>().ReverseMap();
        CreateMap<SurveyRelative, SurveyRelativeCUDTO>().ReverseMap();
        //Profile
        CreateMap<Domain.Models.ProfileModels.Profile, ProfileDTO>().ReverseMap();
        CreateMap<ProfileFile, ProfileFileDTO>().ReverseMap();
        CreateMap<ProfileFile, ProfileFileCUDTO>().ReverseMap();


    }

    
}