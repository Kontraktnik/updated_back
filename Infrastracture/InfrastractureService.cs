using System.Reflection;
using Application.Contracts.Persistence;
using Application.Contracts.Service;
using Application.DTO.EmailDTO;
using Domain;
using FluentValidation;
using Infrastracture.Contracts.Repositories;
using Infrastracture.Contracts.Service;
using Infrastracture.Database;
using Infrastracture.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastracture;

public static class InfrastractureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AppConfig _config = configuration
            .GetSection("AppConfig")
            .Get<AppConfig>();

        var dbString = _config.DbType == "PostgreSQL"
            ? configuration.GetConnectionString("PostgreSqlConnectionString")
            : configuration.GetConnectionString("MySqlConnectionString");

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        string migrationAssembly = $"Infrastracture.{_config.DbType}Migrations";
        if (_config.DbType == "PostgreSQL")
        {
            services.AddDbContext<AppDbContext>(p => p.UseNpgsql(dbString, x => x.MigrationsAssembly(migrationAssembly)));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(dbString, MySqlServerVersion.AutoDetect(dbString)));
        }

        AppConstant.setJWTSettings(configuration["Jwt:Key"],configuration["Jwt:Issuer"],configuration["Jwt:Audience"]);
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IPhoneNotification, PhoneService>();
        //Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));                        
        services.AddScoped<IAreaRepository, AreaRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IPhoneNotificationRepository, PhoneNotificationRepository>();
        services.AddScoped<IArmyDepartmentRepository, ArmyDepartmentRepository>();
        services.AddScoped<IArmyRankRepository, ArmyRankRepository>();
        services.AddScoped<IArmyTypeRepository, ArmyTypeRepository>();
        services.AddScoped<IArmyRegionRepository, ArmyRegionRepository>();
        services.AddScoped<IDriverLicenseRepository, DriverLicenseRepository>();
        services.AddScoped<IVTShRepository, VTShRepository>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<IRelativeRepository, RelativeRepository>();
        services.AddScoped<IMedicalStatusRepository, MedicalStatusRepository>();
        //Calculation
        services.AddScoped<ICategoryPositionRepository, CategoryPositionRepository>();
        services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
        services.AddScoped<IQualificationRepository, QualificationRepository>();
        services.AddScoped<IServiceYearRepository, ServiceYearRepository>();
        services.AddScoped<ISecretLevelRepository, SecretLevelRepository>();
        services.AddScoped<IRankSalaryRepository, RankSalaryRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IJobYearRepository, JobYearRepository>();
        services.AddScoped<ICalculationRepository, CalculationRepository>();
        //Steps
        services.AddScoped<IStepGroupRepository, StepGroupRepository>();
        services.AddScoped<IStepRepository, StepRepository>();
        services.AddScoped<IStepOrderRepository, StepOrderRepository>();

        //User Management
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        //Vacancy
        services.AddScoped<IVacancyRepository, VacancyRepository>();
        //Survey Repository
        services.AddScoped<ISurveyRepository, SurveyRepository>();
        services.AddScoped<ISurveyRelativeRepository, SurveyRelativeRepository>();
        services.AddScoped<ISurveyDriverRepository, SurveyDriverRepository>();
        services.AddScoped<ISurveyExecutorRepository, SurveyExecutorRepository>();

        //Profile
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProfileFileRepository, ProfileFileRepository>();

        services.AddScoped<IDigitalSignRepository, DigitalSignRepository>();
        services.AddScoped<IDigitalSignInfoRepository, DigitalSignInfoRepository>();
        services.AddScoped<IDigitalSignBinaryRepository, DigitalSignBinaryRepository>();
        services.AddScoped<IDigitalSignAttributeRepository, DigitalSignAttributeRepository>();
        services.AddSingleton(_config);


        return services;
    }
}