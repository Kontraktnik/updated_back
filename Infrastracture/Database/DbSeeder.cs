using Domain.Models.CalculationModels;
using Domain.Models.StepModels;
using Domain.Models.SystemModels;
using Domain.Models.UserModels;
using Infrastracture.Helpers;
using Microsoft.Extensions.Logging;

namespace Infrastracture.Database;

public class DbSeeder
{
    public static async Task MigrateAsync(AppDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            //User Seeder
            if (!context.Roles.Any())
            {
                await context.Roles.AddRangeAsync(getRoles());
                await context.SaveChangesAsync();
            }
            //System data Seeder
            if (!context.Areas.Any())
            {
                await context.Areas.AddRangeAsync(GetAreaRaw());
                await context.SaveChangesAsync();
            }
            if (!context.ArmyDepartments.Any())
            {
                await context.ArmyDepartments.AddRangeAsync(getArmyDepartmentRaw());
                await context.SaveChangesAsync();
            }
            if (!context.VtShes.Any())
            {
                await context.VtShes.AddRangeAsync(getVtShes());
                await context.SaveChangesAsync();
            }
            if (!context.Educations.Any())
            {
                await context.Educations.AddRangeAsync(getEducationsRaw());
                await context.SaveChangesAsync();
            }
            if (!context.ArmyRegions.Any())
            {
                await context.ArmyRegions.AddRangeAsync(GetArmyRegionRaw());
                await context.SaveChangesAsync();
            }
            if (!context.ArmyRanks.Any())
            {
                await context.ArmyRanks.AddRangeAsync(GetArmyRankRaw());
                await context.SaveChangesAsync();
            }
            if (!context.ArmyTypes.Any())
            {
                await context.ArmyTypes.AddRangeAsync(GetArmyTypeRaw());
                await context.SaveChangesAsync();
            }
            if (!context.DriverLicenses.Any())
            {
                await context.DriverLicenses.AddRangeAsync(GetDriverLicense());
                await context.SaveChangesAsync();
            }
            if (!context.Relatives.Any())
            {
                await context.Relatives.AddRangeAsync(getRelativesRaw());
                await context.SaveChangesAsync();
            }
            if (!context.MedicalStatuses.Any())
            {
                await context.MedicalStatuses.AddRangeAsync(getMedicalStatusRaw());
                await context.SaveChangesAsync();
            }
            //Calculation Seeder
            if (!context.CategoryPositions.Any())
            {
                await context.CategoryPositions.AddRangeAsync(getCategoryPositionRaw());
                await context.SaveChangesAsync();
            }
            if (!context.JobCategories.Any())
            {
                await context.JobCategories.AddRangeAsync(getJobCategoryRaw());
                await context.SaveChangesAsync();
            }
            if (!context.ServiceYears.Any())
            {
                await context.ServiceYears.AddRangeAsync(getServiceYearRaw());
                await context.SaveChangesAsync();
            }
            if (!context.SecretLevels.Any())
            {
                await context.SecretLevels.AddRangeAsync(getSecretLevelRaw());
                await context.SaveChangesAsync();
            }
            if (!context.Qualifications.Any())
            {
                await context.Qualifications.AddRangeAsync(getQualificationRaw());
                await context.SaveChangesAsync();
            }
            if (!context.JobYears.Any())
            {
                await context.JobYears.AddRangeAsync(getJobYearRaw());
                await context.SaveChangesAsync();
            }
            if (!context.RankSalaries.Any())
            {
                await context.RankSalaries.AddRangeAsync(getArmySalaryRaw());
                await context.SaveChangesAsync();
            }
            //Steps
            if (!context.StepGroups.Any())
            {
                await context.StepGroups.AddRangeAsync(getStepGroupRaw());
                await context.SaveChangesAsync();
            }
            if (!context.Steps.Any())
            {
                await context.Steps.AddRangeAsync(getStepsRaw());
                await context.SaveChangesAsync();
            }
            if (!context.StepOrders.Any())
            {
                await context.StepOrders.AddRangeAsync(getStepOrder());
                await context.SaveChangesAsync();
            }
            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(getRawUser());
                await context.SaveChangesAsync();
            }
            
            
            
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<DbSeeder>();
            logger.LogError(ex.Message);
        }
    }


    private static IEnumerable<Role> getRoles()
    {
        return new List<Role>
        {
            new Role()
            {
               Id = AppConstant.AdminRoleId,
                TitleEn = AppConstant.AdminRoleName,
                TitleRu = "Администратор",
                TitleKz = "Администратор",
            },
            new Role()
            {
               Id = AppConstant.DirectorRoleId,
                TitleEn = AppConstant.DirectorName,
                TitleRu = "Руководитель",
                TitleKz = "Руководитель",
            },
            new Role()
            {
               Id = AppConstant.ExecutorRoleId,
                TitleEn = AppConstant.ExecutorName,
                TitleRu = "Исполнитель",
                TitleKz = "Исполнитель",
            },
            new Role()
            {
               Id = AppConstant.KNBRoleId,
                TitleEn = AppConstant.KNBRoleName,
                TitleRu = "КНБ",
                TitleKz = "КНБ",
            },
            new Role()
            {
               Id = AppConstant.MEDRoleId,
                TitleEn = AppConstant.MEDRoleName,
                TitleRu = "Мед",
                TitleKz = "Мед",
            },
            new Role()
            {
               Id = AppConstant.UserRoleId,
                TitleEn = AppConstant.UserRoleName,
                TitleRu = "Пользователь",
                TitleKz = "Пользователь",
            },
            
        };
    }
    private static IEnumerable<Area> GetAreaRaw()
    {
        return new List<Area>
        {
            new Area()
            {
               //Id = 1,
                TitleEn = "Astana",
                TitleKz = "Астана",
                TitleRu = "Астана"
            },
            new Area()
            {
               //Id = 2,
                TitleEn = "Almaty",
                TitleKz = "Алматы",
                TitleRu = "Алматы"
            },
            new Area()
            {
               //Id = 3,
                TitleEn = "Shymkent",
                TitleKz = "Шымкент",
                TitleRu = "Шымкент"
            },
            new Area()
            {
               //Id = 4,
                TitleEn = "Abai Region",
                TitleKz = "Абай облысы",
                TitleRu = "Абайская область",
                
            },
            new Area()
            {
               //Id = 5,
                TitleEn = "Aqmola Region",
                TitleKz = "Ақмола облысы",
                TitleRu = "Акмолинская область",
            },
            new Area()
            {
               //Id = 6,
                TitleEn = "Aktobe Region",
                TitleKz = "Ақтөбе облысы",
                TitleRu = "Актюбинская область",
            },
            new Area()
            {
               //Id = 7,
                TitleEn = "Almaty Region",
                TitleKz = "Алматы облысы",
                TitleRu = "Алматинская область",
            },
            new Area()
            {
               //Id = 8,
                TitleEn = "Atyrau Region",
                TitleKz = "Атырау облысы",
                TitleRu = "Атырауская область",
            },
            new Area()
            {
               //Id = 9,
                TitleEn = "East Kazakhstan Region",
                TitleKz = "Шығыс Қазақстан облысы",
                TitleRu = "Восточно-Казахстанская область",
            },
            new Area()
            {
               //Id = 10,
                TitleEn = "Jambyl Region",
                TitleKz = "Жамбыл облысы",
                TitleRu = "Жамбылская область",
            },
            new Area()
            {
               //Id = 11,
                TitleEn = "Jetisu Region",
                TitleKz = "Жетісу облысы",
                TitleRu = "Жетысуская область",
            },
            new Area()
            {
               //Id = 12,
                TitleEn = "West Kazakhstan Region",
                TitleKz = "Батыс Қазақстан облысы",
                TitleRu = "Западно-Казахстанская область",
            },
            new Area()
            {
               //Id = 13,
                TitleEn = "Karagandy Region",
                TitleKz = "Қарағанды облысы",
                TitleRu = "Карагандинская область",
            },
            new Area()
            {
               //Id = 14,
                TitleEn = "Kostanay Region",
                TitleKz = "Қостанай облысы",
                TitleRu = "Костанайская область",
            },
            new Area()
            {
               //Id = 15,
                TitleEn = "Kyzylorda Region",
                TitleKz = "Қызылорда облысы",
                TitleRu = "Кызылординская область",
            },
            new Area()
            {
               //Id = 16,
                TitleEn = "Mangistau Region",
                TitleKz = "Маңғыстау облысы",
                TitleRu = "Мангистауская область",
            },
            new Area()
            {
               //Id = 17,
                TitleEn = "Pavlodar Region",
                TitleKz = "Павлодар облысы",
                TitleRu = "Павлодарская область",
            },
            new Area()
            {
               //Id = 18,
                TitleEn = "North Kazakhstan Region",
                TitleKz = "Солтүстік Қазақстан облысы",
                TitleRu = "Северо-Казахстанская область",
            },
            new Area()
            {
               //Id = 19,
                TitleEn = "Turkestan Region",
                TitleKz = "Түркістан облысы",
                TitleRu = "Туркестанская область",
            },
            new Area()
            {
               //Id = 20,
                TitleEn = "Ulytau Region",
                TitleKz = "Ұлытау облысы",
                TitleRu = "Улытауская область",
            },
        };
    }

    private static IEnumerable<ArmyRegion> GetArmyRegionRaw()
    {
        return new List<ArmyRegion>()
        {
            new ArmyRegion()
            {
               //Id = 1,
                TitleEn = "RC 'Астана'",
                TitleKz = "АҚ 'Астана'",
                TitleRu = "РгК 'Астана'"
            },
            new ArmyRegion()
            {
               //Id = 2,
                TitleEn = "RC 'Юг'",
                TitleKz = "АҚ 'Юг'",
                TitleRu = "РгК 'Юг'"
            },
            new ArmyRegion()
            {
               //Id = 3,
                TitleEn = "RC 'Восток'",
                TitleKz = "АҚ 'Восток'",
                TitleRu = "РгК 'Восток'"
            },
            new ArmyRegion()
            {
               //Id = 4,
                TitleEn = "RC 'Запад'",
                TitleKz = "АҚ 'Запад'",
                TitleRu = "РгК 'Запад'"
            },
           
            
        };
    }

    private static IEnumerable<ArmyType> GetArmyTypeRaw()
    {
        return new List<ArmyType>()
        {
            new ArmyType()
            {
               //Id = 1,
                TitleEn = "Ground forces",
                TitleKz = "Құрлық әскерлері",
                TitleRu = "Сухопутные войска",
            },
            new ArmyType()
            {
               //Id = 2,
                TitleEn = "Air Defense Forces",
                TitleKz = "Әуе қорғанысы күштері",
                TitleRu = "Силы воздушной обороны",
            },
            new ArmyType()
            {
               //Id = 3,
                TitleEn = "Navy",
                TitleKz = "Әскери-теңіз флоты",
                TitleRu = "Военно-морской флот",
            },
            new ArmyType()
            {
               //Id = 4,
                TitleEn = "Other BMC",
                TitleKz = "Басқа ӘБО",
                TitleRu = "Другие ОВУ",
            },
        };
    }

    private static IEnumerable<DriverLicense> GetDriverLicense()
    {
        return new List<DriverLicense>()
        {
            new DriverLicense()
            {
               //Id = 1,
                TitleEn = "A",
                TitleRu = "A",
                TitleKz = "A",
            },
            new DriverLicense()
            {
               //Id = 2,
                TitleEn = "B",
                TitleRu = "B",
                TitleKz = "B",
            },
            new DriverLicense()
            {
               //Id = 3,
                TitleEn = "C",
                TitleRu = "C",
                TitleKz = "C",
            },
            new DriverLicense()
            {
               //Id = 4,
                TitleEn = "D",
                TitleRu = "D",
                TitleKz = "D",
            },
            new DriverLicense()
            {
               //Id = 5,
                TitleEn = "BE",
                TitleRu = "BE",
                TitleKz = "BE",
            },
            new DriverLicense()
            {
               //Id = 6,
                TitleEn = "CE",
                TitleRu = "CE",
                TitleKz = "CE",
            },
            new DriverLicense()
            {
               //Id = 7,
                TitleEn = "DE",
                TitleRu = "DE",
                TitleKz = "DE",
            },
            new DriverLicense(){
               //Id = 8,
                TitleEn = "A1",
                TitleRu = "A1",
                TitleKz = "A1",
            },
            new DriverLicense()
            {
               //Id = 9,
                TitleEn = "B1",
                TitleRu = "B1",
                TitleKz = "B1",
            },
            new DriverLicense()
            {
               //Id = 10,
                TitleEn = "C1",
                TitleRu = "C1",
                TitleKz = "C1",
            },
            new DriverLicense()
            {
               //Id = 11,
                TitleEn = "D1",
                TitleRu = "D1",
                TitleKz = "D1",
            },
            new DriverLicense()
            {
               //Id = 12,
                TitleEn = "C1E",
                TitleRu = "C1E",
                TitleKz = "C1E",
            },
            new DriverLicense()
            {
               //Id = 13,
                TitleEn = "D1E",
                TitleRu = "D1E",
                TitleKz = "D1E",
            },
            new DriverLicense()
            {
               //Id = 14,
                TitleEn = "Tm",
                TitleRu = "Tm",
                TitleKz = "Tm",
            },
            new DriverLicense()
            {
               //Id = 15,
                TitleEn = "Tb",
                TitleRu = "Tb",
                TitleKz = "Tb",
            },
        };
    }
    
    
    private static IEnumerable<ArmyRank> GetArmyRankRaw()
    {
        return new List<ArmyRank>()
        {
            new ArmyRank()
            {
               //Id = 1,
                TitleEn = "Private/Sailor",
                TitleRu = "Рядовой/Матрос",
                TitleKz = "Қатардағы Жауынгер / Матрос",
            },
            new ArmyRank()
            {
               //Id = 2,
                TitleEn = "Corporal/Senior Sailor",
                TitleRu = "Ефрейтор/Старший матрос",
                TitleKz = "Ефрейтор / Аға матрос",
            },
            new ArmyRank()
            {
               //Id = 3,
                TitleEn = "Junior Sergeant/Foreman of the second article",
                TitleRu = "Младший Сержант/Старшина второй статьи",
                TitleKz = "Кіші Сержант/Екінші сатылы Старшина",
            },
            new ArmyRank()
            {
               //Id = 4,
                TitleEn = "Junior Sergeant/Foreman of the second article",
                TitleRu = "Сержант/Старшина первой статьи",
                TitleKz = "Кіші сержант/Бірінші сатылы Старшина",
            },
            new ArmyRank()
            {
               //Id = 5,
                TitleEn = "Senior Sergeant/Chief Petty Officer",
                TitleRu = "Старший сержант/Главный старшина",
                TitleKz = "Аға сержант / Бас старшина",
            },
            new ArmyRank()
            {
               //Id = 6,
                TitleEn = "Sergeant Third Class/Petty officer of the third class",
                TitleRu = "Сержант третьего класса/Старшина третьего класса",
                TitleKz = "Үшінші сыныпты Сержант / Үшінші сыныпты Старшина",
            },
            new ArmyRank()
            {
               //Id = 7,
                TitleEn = "Sergeant Second Class/Petty officer of the second class",
                TitleRu = "Сержант второго класса/Старшина второго класса",
                TitleKz = "Екінші сыныпты Сержант / Екінші сыныпты Старшина",
            },
            new ArmyRank()
            {
               //Id = 8,
                TitleEn = "Sergeant First Class/Petty Officer First Class",
                TitleRu = "Сержант первого класса/Старшина первого класса",
                TitleKz = "Бірінші сыныпты Сержант / бірінші сыныпты Старшина",
            },
            new ArmyRank()
            {
               //Id = 9,
                TitleEn = "Staff Sergeant/Staff Sergeant Major",
                TitleRu = "Штаб-сержант/Штаб-старшина",
                TitleKz = "Штаб-сержант/Штаб-старшина",
            },
            new ArmyRank()
            {
               //Id = 10,
                TitleEn = "Master Sergeant/Master Foreman",
                TitleRu = "Мастер-сержант/Мастер-старшина",
                TitleKz = "Мастер-сержант /Мастер-старшина",
            },
            new ArmyRank()
            {
               //Id = 11,
                TitleEn = "Lieutenant/Lieutenant",
                TitleRu = "Лейтенант/Лейтенант",
                TitleKz = "Лейтенант/Лейтенант",
            },
            new ArmyRank()
            {
               //Id = 12,
                TitleEn = "Senior Lieutenant/Senior Lieutenant",
                TitleRu = "Старший лейтенант/Старший лейтенант",
                TitleKz = "Аға лейтенант/Аға лейтенант",
            },
            new ArmyRank()
            {
               //Id = 13,
                TitleEn = "Captain/Captain-Lieutenant",
                TitleRu = "Капитан/Капитан-лейтенант",
                TitleKz = "Капитан/Капитан-лейтенант",
            },
            new ArmyRank()
            {
               //Id = 14,
                TitleEn = "Major/Captain of the third rank",
                TitleRu = "Майор/Капитан третьего ранга",
                TitleKz = "Майор/Үшінші дәрежелі Капитан",
            },
            
            new ArmyRank()
            {
               //Id = 15,
                TitleEn = "Lieutenant Colonel/Captain of the second rank",
                TitleRu = "Подполковник/Капитан второго ранга",
                TitleKz = "Подполковник/Eкінші дәрежелі Капитан",
            },
            new ArmyRank()
            {
               //Id = 16,
                TitleEn = "Colonel/Captain of the first rank",
                TitleRu = "Полковник/Капитан первого ранга",
                TitleKz = "Полковник / Бірінші дәрежелі Капитан",
            },
        };
    }

    private static IEnumerable<ArmyDepartment> getArmyDepartmentRaw()
    {
        return new List<ArmyDepartment>
        {
            new ArmyDepartment
            {
               //Id = 1,
                TitleEn = "Ministry of Defense of the Republic of Kazakhstan",
                TitleRu = "Министерство обороны Республики Казахстан",
                TitleKz = "Қазақстан Республикасы Қорғаныс Министрлігі",
            }
        };
    }
    
    private static IEnumerable<VTSh> getVtShes()
    {
        return new List<VTSh>
        {
            new VTSh()
            {
               //Id = 1,
                TitleEn = "ҚР ҚМ ӘТМ Ақтөбе филиалы",
                TitleRu = "Актюбинский филиал ВТШ МО РК",
                TitleKz = "Aktobe branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 2,
                TitleEn = "ҚР ҚМ ӘТМ Алматы филиалы",
                TitleRu = "Алматинский филиал ВТШ МО РК",
                TitleKz = "Almaty branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 3,
                TitleEn = "ҚР ҚМ ӘТМ Атырау филиалы",
                TitleRu = "Атырауский филиал ВТШ МО РК",
                TitleKz = "Atyrau branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 4,
                TitleEn = "ҚР ҚМ ӘТМ Қарағанды филиалы",
                TitleRu = "Карагандинский филиал ВТШ МО РК",
                TitleKz = "Karaganda branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 5,
                TitleEn = "ҚР ҚМ ӘТМ Қызылорда филиалы",
                TitleRu = "Кызылординский филиал ВТШ МО РК",
                TitleKz = "Kyzylorda branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 6,
                TitleEn = "ҚР ҚМ ӘТМ Павлодар филиалы",
                TitleRu = "Павлодарский филиал ВТШ МО РК",
                TitleKz = "Pavlodar branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 7,
                TitleEn = "ҚР ҚМ ӘТМ Петропавл филиалы",
                TitleRu = "Петропавловский филиал ВТШ МО РК",
                TitleKz = "Petropavlovsk branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 8,
                TitleEn = "ҚР ҚМ ӘТМ Семей филиалы",
                TitleRu = "Семипалатинский филиал ВТШ МО РК",
                TitleKz = "Semipalatinsk branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 9,
                TitleEn = "ҚР ҚМ ӘТМ Талдықорған филиалы",
                TitleRu = "Талдыкорганский филиал ВТШ МО РК",
                TitleKz = "Taldykorgan branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 10,
                TitleEn = "ҚР ҚМ ӘТМ Тараз филиалы",
                TitleRu = "Таразский филиал ВТШ МО РК",
                TitleKz = "Taraz branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 11,
                TitleEn = "ҚР ҚМ ӘТМ Орал филиалы",
                TitleRu = "Уральский филиал ВТШ МО РК",
                TitleKz = "Ural branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 12,
                TitleEn = "ҚР ҚМ ӘТМ Өскемен филиалы",
                TitleRu = "Усть-Каменогорский филиал ВТШ МО РК",
                TitleKz = "Ust-Kamenogorsk branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 13,
                TitleEn = "ҚР ҚМ ӘТМ Шымкент филиалы",
                TitleRu = "Шымкентский филиал ВТШ МО РК",
                TitleKz = "Shymkent branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
            new VTSh()
            {
               //Id = 14,
                TitleEn = "ҚР ҚМ ӘТМ Щучье филиалы",
                TitleRu = "Щучинский филиал ВТШ МО РК",
                TitleKz = "Shchuchinsky branch of the MTS of the Ministry of Defense of the Republic of Kazakhstan",
            },
        };
    }

    private static IEnumerable<Education> getEducationsRaw()
    {
        return new List<Education>
        {
            new Education()
            {
               //Id = 1,
                TitleEn = "Preschool",
                TitleRu = "Дошкольное",
                TitleKz = "Мектепке дейінгі",
            },
            new Education()
            {
               //Id = 2,
                TitleEn = "Basic general secondary (9 grades)",
                TitleRu = "Основное общее среднее (9 классов)",
                TitleKz = "Негізгі жалпы орта (9 сынып)",
            },
            new Education()
            {
               //Id = 3,
                TitleEn = "General secondary (11 grades)",
                TitleRu = "Общее среднее (11 классов)",
                TitleKz = "Жалпы орта (11 сынып)",
            },
            new Education()
            {
               //Id = 4,
                TitleEn = "Incomplete vocational secondary (Prof.schools, Prof.lyceums, colleges)",
                TitleRu = "Неоконченное профессиональное среднее (проф.школы, проф.лицеи, колледжи)",
                TitleKz = "Аяқталмаған кәсіптік орта (кәсіптік мектептер, кәсіптік лицейлер, колледждер)",
            },
            new Education()
            {
               //Id = 5,
                TitleEn = "Professional secondary (Prof.schools, Prof.lyceums, colleges)",
                TitleRu = "Профессиональное среднее (проф.школы, проф.лицеи, колледжи)",
                TitleKz = "Кәсіптік орта (кәсіптік мектептер, кәсіптік лицейлер, колледждер)",
            },
            new Education()
            {
               //Id = 6,
                TitleEn = "Unfinished higher education",
                TitleRu = "Неоконченное высшее",
                TitleKz = "Аяқталмаған жоғары",
            },
            new Education()
            {
               //Id = 7,
                TitleEn = "Higher education",
                TitleRu = "Высшее",
                TitleKz = "Жоғары",
            },
            new Education()
            {
               //Id = 8,
                TitleEn = "Postgraduate Higher education",
                TitleRu = "Послевузовское Высшее",
                TitleKz = "Жоғары оқу орнынан кейінгі білім",
            },
        };

    }

    //Calculations Raw Data
    
    private static IEnumerable<CategoryPosition> getCategoryPositionRaw()
    {
        return new List<CategoryPosition>
        {
            new CategoryPosition()
            {
               //Id = 1,
                TitleEn = "Head of Division",
                TitleRu = "Начальник подразделения",
                TitleKz = "Бөлімше бастығы",
            },
            new CategoryPosition()
            {
               //Id = 2,
                TitleEn = "Senior Specialist",
                TitleRu = "Старший специалист",
                TitleKz = "Аға маман",
            },
            new CategoryPosition()
            {
               //Id = 3,
                TitleEn = "Specialist",
                TitleRu = "Специалист",
                TitleKz = "Маман",
            },
            new CategoryPosition()
            {
               //Id = 4,
                TitleEn = "Senior soldier, Senior sailor",
                TitleRu = "Старший солдат, старший матрос",
                TitleKz = "Аға сарбаз, аға матрос",
            },
            new CategoryPosition()
            {
               //Id = 5,
                TitleEn = "Soldier, sailor",
                TitleRu = "Солдат, матрос",
                TitleKz = "Сарбаз, матрос",
            },
        };
    }
    
    private static IEnumerable<JobCategory> getJobCategoryRaw()
    {
        return new List<JobCategory>
        {
            new JobCategory()
            {
               //Id = 1,
                TitleEn = "I",
                TitleRu = "I",
                TitleKz = "I",
            },
            new JobCategory()
            {
               //Id = 2,
                TitleEn = "II",
                TitleRu = "II",
                TitleKz = "II",
            },
            new JobCategory()
            {
               //Id = 3,
                TitleEn = "III",
                TitleRu = "III",
                TitleKz = "III",
            },
            new JobCategory()
            {
               //Id = 4,
                TitleEn = "IV",
                TitleRu = "IV",
                TitleKz = "IV",
            },
            new JobCategory()
            {
               //Id = 5,
                TitleEn = "V",
                TitleRu = "V",
                TitleKz = "V",
            },
            new JobCategory()
            {
               //Id = 6,
                TitleEn = "VI",
                TitleRu = "VI",
                TitleKz = "VI",
            },
            new JobCategory()
            {
               //Id = 7,
                TitleEn = "VII",
                TitleRu = "VII",
                TitleKz = "VII",
            },
            new JobCategory()
            {
               //Id = 8,
                TitleEn = "VIII",
                TitleRu = "VIII",
                TitleKz = "VIII",
            },
            new JobCategory()
            {
               //Id = 9,
                TitleEn = "IX",
                TitleRu = "IX",
                TitleKz = "IX",
            },
        };
    }
    
    private static IEnumerable<ServiceYear> getServiceYearRaw()
    {
        return new List<ServiceYear>
        {
            new ServiceYear()
            {
               //Id = 1,
                TitleEn = "Up to a year",
                TitleRu = "До года",
                TitleKz = "Бір жылға дейін",
                Min = 0,
                Max = 1,
            },
            new ServiceYear()
            {
               //Id = 2,
                TitleEn = "From 1 to 2 years",
                TitleRu = "C 1 до 2 лет",
                TitleKz = "1 жастан 2 жасқа дейін",
                Min = 1,
                Max = 2,
            },
            new ServiceYear()
            {
               //Id = 3,
                TitleEn = "From 2 to 3 years",
                TitleRu = "C 2 до 3 лет",
                TitleKz = "2 жастан 3 жасқа дейін",
                Min = 2,
                Max = 3,
            },
            new ServiceYear()
            {
               //Id = 4,
                TitleEn = "From 3 to 5 years",
                TitleRu = "C 3 до 5 лет",
                TitleKz = "3 жастан 5 жасқа дейін",
                Min = 3,
                Max = 5,
            },
            new ServiceYear()
            {
               //Id = 5,
                TitleEn = "From 5 to 7 years",
                TitleRu = "С 5 до 7 лет",
                TitleKz = "5 жастан 7 жасқа дейін",
                Min = 5,
                Max = 7,
            },
            new ServiceYear()
            {
               //Id = 6,
                TitleEn = "From 7 to 9 years old",
                TitleRu = "C 7 до 9 лет",
                TitleKz = "7 жастан 9 жасқа дейін",
                Min = 7,
                Max = 9,
            },
            new ServiceYear()
            {
               //Id = 7,
                TitleEn = "From 9 to 11 years old",
                TitleRu = "C 9 до 11 лет",
                TitleKz = "9 жастан 11 жасқа дейін",
                Min = 9,
                Max = 11,
            },
            new ServiceYear()
            {
               //Id = 8,
                TitleEn = "From 11 to 14 years old",
                TitleRu = "C 11 до 14 лет",
                TitleKz = "11 жастан 14 жасқа дейін",
                Min = 11,
                Max = 14,
            },
            new ServiceYear()
            {
               //Id = 9,
                TitleEn = "From 14 to 17 years",
                TitleRu = "C 14 до 17 лет",
                TitleKz = "14 жастан 17 жасқа дейін",
                Min = 14,
                Max = 17,
            },
            
            new ServiceYear()
            {
               //Id = 10,
                TitleEn = "From 17 to 20 years old",
                TitleRu = "С 17 до 20 лет",
                TitleKz = "17 жастан 20 жасқа дейін",
                Min = 17,
                Max = 20,
            },
            new ServiceYear()
            {
               //Id = 11,
                TitleEn = "Over 20 years",
                TitleRu = "Свыше 20 лет",
                TitleKz = "20 жылдан астам",
                Min = 20,
                Max = 100,
            },
        };
    }
    
    private static IEnumerable<SecretLevel> getSecretLevelRaw()
    {
        return new List<SecretLevel>
        {
            new SecretLevel()
            {
               //Id = 1,
                TitleEn = "Special importance",
                TitleRu = "Особой важности",
                TitleKz = "Аса маңызды",
                Percentage = 5
            },
            new SecretLevel()
            {
               //Id = 2,
                TitleEn = "Top secret",
                TitleRu = "Cовершенно секретно",
                TitleKz = "Өте құпия",
                Percentage = 4
            },
            new SecretLevel()
            {
               //Id = 3,
                TitleEn = "Secret",
                TitleRu = "Cекретно",
                TitleKz = "Құпия",
                Percentage = 3
            },
            new SecretLevel()
            {
               //Id = 4,
                TitleEn = "No Access",
                TitleRu = "Не имеет допуска",
                TitleKz = "Рұқсаты жоқ",
                Percentage = 0
            },
        };
    }

    private static IEnumerable<Qualification> getQualificationRaw()
    {
        return new List<Qualification>
        {
            new Qualification()
            {
               //Id = 1,
                TitleEn = "Master Mentor",
                TitleRu = "Мастер-наставник",
                TitleKz = "Шебер тәлімгер",
                Percentage = 30
            },
            new Qualification()
            {
               //Id = 2,
                TitleEn = "Specialist 1",
                TitleRu = "Специалист 1",
                TitleKz = "Маман 1",
                Percentage = 20
            },
            new Qualification()
            {
               //Id = 3,
                TitleEn = "Specialist 2",
                TitleRu = "Специалист 2",
                TitleKz = "Маман 2",
                Percentage = 10
            },
            new Qualification()
            {
               //Id = 4,
                TitleEn = "Non-Specialist",
                TitleRu = "Не имеет специализации",
                TitleKz = "Маман емес",
                Percentage = 0
            },
        };
    }

    private static IEnumerable<JobYear> getJobYearRaw()
    {
        return new List<JobYear>()
        {
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 1,
                Salary = 68513
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 1,
                Salary = 77751
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 1,
                Salary = 86989
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 1,
                Salary = 96484
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 1,
                Salary = 105721
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 1,
                Salary = 114960
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 1,
                Salary = 124198
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 1,
                Salary = 133435
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 1,
                Salary = 142929
            },
            //year - 2
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 2,
                Salary = 72106
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 2,
                Salary = 81344
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 2,
                Salary = 90582
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 2,
                Salary = 99819
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 2,
                Salary = 109057
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 2,
                Salary = 118552
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 2,
                Salary = 127790
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 2,
                Salary = 137027
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 2,
                Salary = 146265
            },
            //3 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 3,
                Salary = 75442
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 3,
                Salary = 84680
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 3,
                Salary = 94174
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 3,
                Salary = 103412
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 3,
                Salary = 112650
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 3,
                Salary = 121888
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 3,
                Salary = 131125
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 3,
                Salary = 140620
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 3,
                Salary = 149858
            },
            //4 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 4,
                Salary = 79034
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 4,
                Salary = 88272
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 4,
                Salary = 97510
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 4,
                Salary = 106748
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 4,
                Salary = 116242
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 4,
                Salary = 125480
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 4,
                Salary = 134718
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 4,
                Salary = 143956
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 4,
                Salary = 153194
            },
            //5 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 5,
                Salary = 83653
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 5,
                Salary = 92891
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 5,
                Salary = 102129
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 5,
                Salary = 111367
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 5,
                Salary = 120861
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 5,
                Salary = 130099
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 5,
                Salary = 139337
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 5,
                Salary = 148575
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 5,
                Salary = 157812
            },
            //6 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 6,
                Salary = 86989
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 6,
                Salary = 96484
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 6,
                Salary = 105721
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 6,
                Salary = 114959
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 6,
                Salary = 124197
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 6,
                Salary = 133435
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 6,
                Salary = 142929
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 6,
                Salary = 152167
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 6,
                Salary = 161405
            },
            //7 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 7,
                Salary = 91608
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 7,
                Salary = 101102
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 7,
                Salary = 110340
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 7,
                Salary = 119578
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 7,
                Salary = 128816
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 7,
                Salary = 138054
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 7,
                Salary = 147548
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 7,
                Salary = 156786
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 7,
                Salary = 166024
            },
            //8 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 8,
                Salary = 96484
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 8,
                Salary = 105721
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 8,
                Salary = 114959
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 8,
                Salary = 124197
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 8,
                Salary = 133435
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 8,
                Salary = 142929
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 8,
                Salary = 152167
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 8,
                Salary = 161405
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 8,
                Salary = 170643
            },
            //9 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 9,
                Salary = 101102
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 9,
                Salary = 110340
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 9,
                Salary = 119578
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 9,
                Salary = 128816
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 9,
                Salary = 138054
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 9,
                Salary = 147548
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 9,
                Salary = 156786
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 9,
                Salary = 166024
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 9,
                Salary = 175262
            },
            //10 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 10,
                Salary = 106748
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 10,
                Salary = 116242
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 10,
                Salary = 125480
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 10,
                Salary = 134718
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 10,
                Salary = 143956
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 10,
                Salary = 153194
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 10,
                Salary = 162688
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 10,
                Salary = 171926
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 10,
                Salary = 181164
            },
            //11 - years
            new JobYear()
            {
                JobCategoryId = 1,
                ServiceYearId = 11,
                Salary = 111367
            },
            new JobYear()
            {
                JobCategoryId = 2,
                ServiceYearId = 11,
                Salary = 120861
            },
            new JobYear()
            {
                JobCategoryId = 3,
                ServiceYearId = 11,
                Salary = 130099
            },
            new JobYear()
            {
                JobCategoryId = 4,
                ServiceYearId = 11,
                Salary = 139337
            },
            new JobYear()
            {
                JobCategoryId = 5,
                ServiceYearId = 11,
                Salary = 148575
            },
            new JobYear()
            {
                JobCategoryId = 6,
                ServiceYearId = 11,
                Salary = 157812
            },
            new JobYear()
            {
                JobCategoryId = 7,
                ServiceYearId = 11,
                Salary = 167307
            },
            new JobYear()
            {
                JobCategoryId = 8,
                ServiceYearId = 11,
                Salary = 176545
            },
            new JobYear()
            {
                JobCategoryId = 9,
                ServiceYearId = 11,
                Salary = 185783
            },
        };
    }
    
    private static IEnumerable<RankSalary> getArmySalaryRaw()
    {
        return new List<RankSalary>
        {
            new RankSalary()
            {
               //Id = 1,
                ArmyRankId = 1,
                Salary = 10919
            },
            new RankSalary()
            {
               //Id = 2,
                ArmyRankId = 2,
                Salary = 12016
            },
            new RankSalary()
            {
               //Id = 3,
                ArmyRankId = 3,
                Salary = 15007
            },
            new RankSalary()
            {
               //Id = 4,
                ArmyRankId = 4,
                Salary = 16511
            },
            new RankSalary()
            {
               //Id = 5,
                ArmyRankId = 5,
                Salary = 18175
            },
            new RankSalary()
            {
               //Id = 6,
                ArmyRankId = 6,
                Salary = 19980
            },
            new RankSalary()
            {
               //Id = 7,
                ArmyRankId = 7,
                Salary = 21980
            },
            new RankSalary()
            {
               //Id = 8,
                ArmyRankId = 8,
                Salary = 24174
            },
            new RankSalary()
            {
               //Id = 9,
                ArmyRankId = 9,
                Salary = 27802
            },
            new RankSalary()
            {
               //Id = 10,
                ArmyRankId = 10,
                Salary = 30580
            },
        };
    }

    private static IEnumerable<User> getRawUser()
    {
        return new List<User>
        {
            new User()
            {
               //Id = 1,
                RoleId = 1,
                IIN = "000000000000",
                Name = "Admin",
                Surname = "Adminov",
                FullName = "Admin Adminov",
                Email = "admin@gmail.com",
                Phone = "+77777777777",
                Password = "$2a$11$V9NhVSpoFujwuhPmOuSzceAyuM3bmNq9s4MKe4RRBwg/0KvicKQKW",
                Verified = true,
                Status = true
            },
            new User()
            {
               //Id = 2,
                RoleId = 2,
                IIN = "111111111111",
                Name = "Director",
                Surname = "Directorov",
                FullName = "Director Directorov",
                Email = "director@gmail.com",
                Phone = "+77777777771",
                Password = "$2a$11$V9NhVSpoFujwuhPmOuSzceAyuM3bmNq9s4MKe4RRBwg/0KvicKQKW",
                Verified = true,
                Status = true,
                AreaId = 1
            },
            new User()
            {
               //Id = 3,
                RoleId = 3,
                IIN = "222222222222",
                Name = "Executor",
                Surname = "Executorov",
                FullName = "Executor Executorov",
                Email = "executor@gmail.com",
                Phone = "+77777777772",
                Password = "$2a$11$V9NhVSpoFujwuhPmOuSzceAyuM3bmNq9s4MKe4RRBwg/0KvicKQKW",
                Verified = true,
                Status = true,
                AreaId = 1
            },
            new User()
            {
               //Id = 4,
                RoleId = 4,
                IIN = "333333333333",
                Name = "KNB",
                Surname = "KNB",
                FullName = "KNB KNB",
                Email = "KNB@gmail.com",
                Phone = "+77777777774",
                Password = "$2a$11$V9NhVSpoFujwuhPmOuSzceAyuM3bmNq9s4MKe4RRBwg/0KvicKQKW",
                Verified = true,
                Status = true,
                AreaId = 1
            },
            new User()
            {
               //Id = 5,
                RoleId = 5,
                IIN = "444444444444",
                Name = "MED",
                Surname = "MED",
                FullName = "MED MED",
                Email = "MED@gmail.com",
                Phone = "+77777777776",
                Password = "$2a$11$V9NhVSpoFujwuhPmOuSzceAyuM3bmNq9s4MKe4RRBwg/0KvicKQKW",
                Verified = true,
                Status = true,
                AreaId = 1
            },
            new User()
            {
               //Id = 6,
                RoleId = 6,
                IIN = "555555555555",
                Name = "User",
                Surname = "Userov",
                FullName = "User Userov",
                Email = "user@gmail.com",
                Phone = "+77777777778",
                Password = "$2a$11$V9NhVSpoFujwuhPmOuSzceAyuM3bmNq9s4MKe4RRBwg/0KvicKQKW",
                Verified = true,
                Status = true,
            },
        };
    }


    private static IEnumerable<MedicalStatus> getMedicalStatusRaw()
    {
        return new List<MedicalStatus>
        {
            new MedicalStatus()
            {
               //Id = 1,
                Code = "А",
                TitleRu = "А - годен к воинской службе",
                TitleEn = "A - fit for military service",
                TitleKz = "А-әскери қызметке жарамды"
            },
            new MedicalStatus()
            {
               //Id = 2,
                Code = "Б",
                TitleRu = "Б - годен к воинской службе с незначительными ограничениями",
                TitleEn = "B - fit for military service with minor restrictions",
                TitleKz = "Б-шамалы шектеулермен әскери қызметке жарамды"
            },
            new MedicalStatus()
            {
               //Id = 3,
                Code = "В",
                TitleRu = "В - ограниченно годен к воинской службе",
                TitleEn = "B - limited fit for military service",
                TitleKz = "В-әскери қызметке шектеулі жарамды"
            },
            new MedicalStatus()
            {
               //Id = 4,
                Code = "Г",
                TitleRu = "Г - временно не годен к воинской службе",
                TitleEn = "G - temporarily unfit for military service",
                TitleKz = "Г - әскери қызметке уақытша жарамсыз"
            },
            new MedicalStatus()
            {
               //Id = 5,
                Code = "Д",
                TitleRu = "Д - не годен к воинской службе в мирное время, ограниченно годен в военное время",
                TitleEn = "D - not fit for military service in peacetime, limited fit in wartime",
                TitleKz = "Д-бейбіт уақытта әскери қызметке жарамсыз, соғыс уақытында шектеулі"
            },
            new MedicalStatus()
            {
               //Id = 6,
                Code = "Е",
                TitleRu = "Е - не годен к воинской службе с исключением с воинского учета",
                TitleEn = "E - not fit for military service with the exception of military registration",
                TitleKz = "Е - әскери есептен шығара отырып, әскери қызметке жарамсыз"
            },
            new MedicalStatus()
            {
               //Id = 7,
                Code = "НГ",
                TitleRu = "НГ - не годен к воинской службе в видах и родах войск Вооруженных Сил, а также других войск и воинских формирований Республики Казахстан по отдельным военно-учетным специальностям, не годен к поступлению в ВУЗ (школы), не годен к воинской службе с вредными факторами",
                TitleEn = "NG - is not fit for military service in the types and branches of the Armed Forces, as well as other troops and military formations of the Republic of Kazakhstan in certain military accounting specialties, is not fit for admission to a university (school), is not fit for military service with harmful factors",
                TitleKz = "НГ-Қазақстан Республикасы Қарулы Күштері әскерлерінің, сондай-ақ басқа да әскерлері мен әскери құралымдарының жекелеген әскери-есептік мамандықтар бойынша әскери қызметке жарамсыз, ЖОО-ға (мектептерге) түсуге жарамсыз, зиянды факторлары бар әскери қызметке жарамсыз"
            },
            new MedicalStatus()
            {
               //Id = 8,
                Code = "В-ИНД",
                TitleRu = "В-ИНД - годность к воинской службе в Вооруженных Силах, других войсках и воинских формированиях Республики Казахстан определяется индивидуально, и предусматривает категорию годности Б или В",
                TitleEn = "В-ИНД - the fitness for military service in the Armed Forces, other troops and military formations of the Republic of Kazakhstan is determined individually, and provides for the category of fitness B or C",
                TitleKz = "В-инд -Қазақстан Республикасының Қарулы Күштерінде, басқа да әскерлері мен әскери құралымдарында әскери қызметке жарамдылығы жеке айқындалады және Б немесе В жарамдылығы санатын көздейді"
            },
            new MedicalStatus()
            {
               //Id = 9,
                Code = "ИНД ",
                TitleRu = "ИНД - годность к воинской службе в видах и родах войск Вооруженных Сил, а также других войск и воинских формирований Республики Казахстан по отдельным военно-учетным специальностям определяется индивидуально.",
                TitleEn = "ИНД - the suitability for military service in the types and branches of the Armed Forces, as well as other troops and military formations of the Republic of Kazakhstan for certain military accounting specialties is determined individually.",
                TitleKz = "ИНД -Қазақстан Республикасы Қарулы Күштері әскерлерінің, сондай-ақ басқа да әскерлері мен әскери құралымдарының жекелеген әскери-есептік мамандықтар бойынша әскери қызметке жарамдылығы жеке айқындалады."
            },
        };
    }


    private static IEnumerable<Relative> getRelativesRaw()
    {
        return new List<Relative>
        {
            new Relative()
            {
                TitleRu = "Отец",
                TitleEn = "Father",
                TitleKz = "Әке",
            },
            new Relative()
            {
                TitleRu = "Мать",
                TitleEn = "Mother",
                TitleKz = "Ана",
            },
            new Relative()
            {
                TitleRu = "Брат",
                TitleEn = "Brother",
                TitleKz = "Аға",
            },
            new Relative()
            {
                TitleRu = "Сестра",
                TitleEn = "Sister",
                TitleKz = "Әпке",
            },
            new Relative()
            {
                TitleRu = "Супруга",
                TitleEn = "Spouse",
                TitleKz = "Жұбайы",
            },
            new Relative()
            {
                TitleRu = "Супруг",
                TitleEn = "Spouse",
                TitleKz = "Жұбайы",
            },
            new Relative()
            {
                TitleRu = "Сын",
                TitleEn = "Son",
                TitleKz = "Ұлы",
            },
            new Relative()
            {
                TitleRu = "Дочь",
                TitleEn = "Daughter",
                TitleKz = "Қызы",
            },
            new Relative()
            {
                TitleRu = "Другое",
                TitleEn = "Other",
                TitleKz = "Басқа",
            },
        };
    }


    private static IEnumerable<StepGroup> getStepGroupRaw()
    {
        return new List<StepGroup>()
        {
            new StepGroup()
            {
               Id = AppConstant.SendedStateGroup,
                TitleRu = "Документы отправлены",
                TitleKz = "Құжаттар жіберілді",
                TitleEn = "The documents have been sent",
                Order = 1,
            },
            new StepGroup()
            {
               Id = AppConstant.AcceptedStateGroup,
                TitleRu = "Принято в работу",
                TitleKz = "Жұмысқа қабылданды",
                TitleEn = "Accepted for work",
                Order = 2,
            },
            new StepGroup()
            {
               Id = AppConstant.SpecialStateGroup,
                TitleRu = "Спецпроверка",
                TitleKz = "Арнайы тексеру",
                TitleEn = "Special check",
                Order = 3,
            },
            new StepGroup()
            {
               Id = AppConstant.MedicalStateGroup,
                TitleRu = "Медобследование",
                TitleKz = "Медициналық тексеру",
                TitleEn = "Medical examination",
                Order = 4,
            },
            new StepGroup()
            {
               Id = AppConstant.OfferStateGroup,
                TitleRu = "Подписание",
                TitleKz = "Қол қою",
                TitleEn = "Signing",
                Order = 5
            },
        };

    }

    private static IEnumerable<Step> getStepsRaw()
    {
        return new List<Step>()
        {
            new Step()
            {
               Id = AppConstant.SendedState,
                StepGroupId = AppConstant.SendedStateGroup,
                TitleRu = "Документы отправлены",
                TitleKz = "Құжаттар жіберілді",
                TitleEn = "The documents have been sent",
                RequestedRoleId = AppConstant.UserRoleId,
                ConfirmedRoleId = AppConstant.DirectorRoleId,
                IsFirst = true,
                IsLast = false,
                DayLimit = 5,
                
            },
            new Step()
            {
               Id = AppConstant.AcceptedState,
                StepGroupId = AppConstant.AcceptedStateGroup,
                TitleRu = "Принято в работу",
                TitleKz = "Жұмысқа қабылданды",
                TitleEn = "Accepted for work",
                RequestedRoleId = AppConstant.DirectorRoleId,
                ConfirmedRoleId = AppConstant.ExecutorRoleId,
                IsFirst = false,
                IsLast = false,
                DayLimit = 5,
            },
            new Step()
            {
               Id = AppConstant.SpecialState,
                StepGroupId = AppConstant.SpecialStateGroup,
                TitleRu = "Спецпроверка",
                TitleKz = "Арнайы тексеру",
                TitleEn = "Special check",
                RequestedRoleId = AppConstant.ExecutorRoleId,
                ConfirmedRoleId = AppConstant.KNBRoleId,
                IsFirst = false,
                IsLast = false,
                DayLimit = 5,
            },
            new Step()
            {
               Id = AppConstant.PreparedMedState,
                StepGroupId = AppConstant.MedicalStateGroup,
                TitleRu = "Подготовка к медобследованию",
                TitleKz = "Медициналық тексеруге дайындық",
                TitleEn = "Preparation for medical examination",
                RequestedRoleId = AppConstant.ExecutorRoleId,
                ConfirmedRoleId = AppConstant.UserRoleId,
                IsFirst = false,
                IsLast = false,
                DayLimit = 5,
                
            },
            new Step()
            {
               Id = AppConstant.MedState,
                StepGroupId = AppConstant.MedicalStateGroup,
                TitleRu = "Прохождение медобследования",
                TitleKz = "Медициналық тексеруден өту",
                TitleEn = "Passing a medical examination",
                RequestedRoleId = AppConstant.UserRoleId,
                ConfirmedRoleId = AppConstant.MEDRoleId,
                IsFirst = false,
                IsLast = false,
                DayLimit = 5,
            },
            new Step()
            {
               Id = AppConstant.PreparePsychoMedState,
                StepGroupId = AppConstant.MedicalStateGroup,
                TitleRu = "Подготовка к психологическому тестированию",
                TitleKz = "Психологиялық тестілеуге дайындық",
                TitleEn = "Preparation for psychological testing",
                RequestedRoleId = AppConstant.ExecutorRoleId,
                ConfirmedRoleId = AppConstant.UserRoleId,
                IsFirst = false,
                IsLast = false,
                DayLimit = 5,
            },
            new Step()
            {
               Id = AppConstant.PsychoMedState,
                StepGroupId = AppConstant.MedicalStateGroup,
                TitleRu = "Прохождение психологического тестирования",
                TitleKz = "Психологиялық тестілеуден өту",
                TitleEn = "Passing psychological testing",
                RequestedRoleId = AppConstant.UserRoleId,
                ConfirmedRoleId = AppConstant.MEDRoleId,
                IsFirst = false,
                IsLast = false,
                DayLimit = 5,
            },
            new Step()
            {
               Id = AppConstant.OfferState,
                StepGroupId = AppConstant.OfferStateGroup,
                TitleRu = "Предложение вакантной должности",
                TitleKz = "Бос лауазымға ұсыныс",
                TitleEn = "Offer of a vacant position",
                RequestedRoleId = AppConstant.ExecutorRoleId,
                ConfirmedRoleId = AppConstant.UserRoleId,
                IsFirst = false,
                IsLast = false,
                DayLimit = 5,
            },
            new Step()
            {
               Id = AppConstant.SigningState,
                StepGroupId = AppConstant.OfferStateGroup,
                TitleRu = "Подписание договора",
                TitleKz = "Шартқа қол қою",
                TitleEn = "Signing the contract",
                RequestedRoleId = AppConstant.ExecutorRoleId,
                ConfirmedRoleId = AppConstant.DirectorRoleId,
                IsFirst = false,
                IsLast = true,
                DayLimit = 5,
            },
        };

    }

    private static IEnumerable<StepOrder> getStepOrder()
    {
        return new List<StepOrder>()
        {
            new StepOrder()
            {
                StepId = AppConstant.SendedState,
                NextStepId = AppConstant.AcceptedState,
            },
            new StepOrder()
            {
                StepId = AppConstant.AcceptedState,
                PreviousStepId = AppConstant.SendedState,
                NextStepId = AppConstant.SpecialState,
           },
            new StepOrder()
            {
                StepId = AppConstant.SpecialState,
                PreviousStepId = AppConstant.AcceptedState,
                NextStepId = AppConstant.PreparedMedState,
            },
            new StepOrder()
            {
                StepId = AppConstant.PreparedMedState,
                PreviousStepId = AppConstant.SpecialState,
                NextStepId = AppConstant.MedState,
            },
            new StepOrder()
            {
                StepId = AppConstant.MedState,
                PreviousStepId = AppConstant.PreparedMedState,
                NextStepId = AppConstant.PreparePsychoMedState,
            },
            new StepOrder()
            {
                StepId = AppConstant.PreparePsychoMedState,
                PreviousStepId = AppConstant.MedState,
                NextStepId = AppConstant.PsychoMedState,
            },
            new StepOrder()
            {
                StepId = AppConstant.PsychoMedState,
                PreviousStepId = AppConstant.PreparePsychoMedState,
                NextStepId = AppConstant.OfferState,
            },
            new StepOrder()
            {
                StepId = AppConstant.OfferState,
                PreviousStepId = AppConstant.PsychoMedState,
                NextStepId = AppConstant.SigningState,
            },
            new StepOrder()
            {
                StepId = AppConstant.SigningState,
                PreviousStepId = AppConstant.OfferState,
            },
            
        };
    }


}