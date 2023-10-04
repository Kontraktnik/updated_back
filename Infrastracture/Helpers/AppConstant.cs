using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastracture.Helpers;

public class AppConstant
{
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

    //State Group
    public static int SendedStateGroup = 1;
    public static int AcceptedStateGroup = 2;
    public static int SpecialStateGroup = 3;
    public static int MedicalStateGroup = 4;
    public static int OfferStateGroup = 5;

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


    public static string JWTSecret { get; set; }
    public static string JWTIssuer { get; set; }
    public static string JWTAudience { get; set; }

    public static int JWTExpiredMin { get; set; } = 60;

    public static void setJWTSettings(string key,string issuer,string audience)
    {
        JWTSecret = key;
        JWTIssuer = issuer;
        JWTAudience = audience;
    }

    public static readonly string UploadsFolder = "uploads";

    public static string RegisterCode = "Registration";


    public const int DigitalSignAttributeField = 1;
    public const int DigitalSignAttributeFile = 2;
    public const string DigitalSignEnding = @"
";

}