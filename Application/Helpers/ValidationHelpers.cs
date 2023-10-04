namespace Application.Helpers;

public class ValidationHelpers
{
    //Model Settings
    public static int AreaMaxId = 20;
    public static int ArmyDepartmentMax = 1;
    public static int ArmyRegionMax = 4;
    public static int ArmyRankMax = 16;
    public static int ArmyTypeMax = 4;
    public static int CategoryPositionMax = 5;
    public static int DriverLicenseMax = 15;
    public static int EducationMax = 8;
    public static int JobCategoryMax = 9;
    public static int MedicalStatusMax = 9;
    public static int QualificationStatusMax = 4;
    public static int RelativeMax = 9;
    public static int SecretLevelMax = 4;
    public static int ServiceYearMax = 11;
    public static int VtshMax = 14;

    public const string AdminRoleName = "Administrator";
    public static long AdminRoleId = 1;

    public const string DirectorName = "Director";
    public static long DirectorRoleId= 2;
    
    public const string ExecutorName = "Executor";
    public static long ExecutorRoleId = 3;
    
    public const string KNBRoleName = "KNB";
    public static long KNBRoleId = 4;
    
    public const string MEDRoleName = "MED";
    public static long MEDRoleId = 5;
    
    public const string UserRoleName = "User";
    public static long UserRoleId = 6;

    public static long[] RequiredAreaByRoles = new long[] { 3, 4, 5 };
    
    //State
    public static int SendedState = 1;
    public static int AcceptedState = 2;
    public static int SpecialState = 3;
    public static int PreparedMedState = 4;
    public static int MedState = 5;
    public static int PreparePsychoMedState = 6;
    public static int PsychoMedState = 7;
    public static int OfferState = 8;
    public static int SigningState = 9;
}