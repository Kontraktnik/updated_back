using System.Reflection;
using Domain.Models.CalculationModels;
using Domain.Models.DigitalSignModels;
using Domain.Models.NotificationModels;
using Domain.Models.ProfileModels;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Domain.Models.VacancyModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    //System Model
    
    public DbSet<ArmyDepartment> ArmyDepartments { get; set; }
    
    public DbSet<ArmyRank> ArmyRanks { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<ArmyRegion> ArmyRegions { get; set; }
    public DbSet<ArmyType> ArmyTypes { get; set; }
    public DbSet<DriverLicense> DriverLicenses { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<VTSh> VtShes { get; set; }
    
    public DbSet<MedicalStatus> MedicalStatuses{ get; set; }
    
    public DbSet<Relative> Relatives{ get; set; }

    


    //User Model
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    
    //Calculation Model
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<CategoryPosition> CategoryPositions { get; set; }
    public DbSet<ServiceYear> ServiceYears { get; set; }
    public DbSet<SecretLevel> SecretLevels { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<RankSalary> RankSalaries { get; set; }
    public DbSet<JobYear> JobYears { get; set; }
    public DbSet<Position> Positions { get; set; }
    
    public DbSet<Vacancy> Vacancies { get; set; }

    //Step
    public DbSet<StepGroup> StepGroups { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<StepOrder> StepOrders { get; set; }

    //Surveys
    public DbSet<Survey> Surveys { get; set; }
    
    public DbSet<SurveyDriver> SurveyDrivers { get; set; }
    public DbSet<SurveyExecutor> SurveyExecutors { get; set; }
    public DbSet<SurveyRelative> SurveyRelatives { get; set; }

    //DigitalSigns
    public DbSet<DigitalSign> DigitalSigns { get; set; }
    public DbSet<DigitalSignBinary> DigitalSignBinaries { get; set; }
    public DbSet<DigitalSignInfo> DigitalSignInfos { get; set; }
    public DbSet<DigitalSignAttribute> DigitalSignAttributes { get; set; }

    //Application
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<ProfileFile> ProfileFiles { get; set; }

    
    public DbSet<PhoneNotification> PhoneNotifications { get; set; }


}