using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        //AddSignalR
        services.AddSignalR();
        
        
        //Localization
        services.AddLocalization(options =>
        {
            options.ResourcesPath = "";
        });
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("kk"),
                new CultureInfo("ru"),
                new CultureInfo("en"),
                
            };
            
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        
        //Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
        
        
        // Enable Swagger for Authentication
        services.AddSwaggerGen(swagger =>  
        {  
            //This is to generate the Default UI of Swagger Documentation  
            swagger.SwaggerDoc("v1", new OpenApiInfo  
            {   
                Version= "v1",   
                Title = "JWT Token Authentication API",  
                Description="ASP.NET Core 6.0 Web API" });  
            // To Enable authorization using Swagger (JWT)  
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
            {  
                Name = "Authorization",  
                Type = SecuritySchemeType.ApiKey,  
                Scheme = "Bearer",  
                BearerFormat = "JWT",  
                In = ParameterLocation.Header,  
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  
            });  
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement  
            {  
                {  
                    new OpenApiSecurityScheme  
                    {  
                        Reference = new OpenApiReference  
                        {  
                            Type = ReferenceType.SecurityScheme,  
                            Id = "Bearer"  
                        }  
                    },  
                    new string[] {}  
  
                }  
            });  
        });  
        //Create Static Folder
        
        
        return services;
    }
}