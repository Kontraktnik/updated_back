using System.Configuration;
using System.Net;
using System.Security.Claims;
using System.Text;
using API.Extensions;
using API.Hubs;
using API.Middlewares;
using Application;
using Domain;
using Infrastracture;
using Infrastracture.Database;
using Infrastracture.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceExtension(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureServices(builder.Configuration);

AppConfig _configuration = builder.Configuration
    .GetSection("AppConfig")
    .Get<AppConfig>();


if (_configuration.UseHttpsRedirection)
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
        options.HttpsPort = 5001;
    });
}

if (_configuration.UseSpaServices && builder.Environment.EnvironmentName != Environments.Development)
{
    builder.Services.AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "wwwroot";
    });
}

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        if (builder.Environment.EnvironmentName != Environments.Development)
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(_configuration.Origins);
        }
        else
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(_configuration.DevOrigins);
        }
    });
});

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
});
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

var app = builder.Build();

if (_configuration.DbType == "PostgreSQL")
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

//app.UseHttpsRedirection();
app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var locOptions = services.GetService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(locOptions.Value);
    if (!Directory.Exists($"{Path.Combine(builder.Environment.ContentRootPath, _configuration.UploadRequestStoragePath)}"))
    {
        Directory.CreateDirectory($"{Path.Combine(builder.Environment.ContentRootPath, _configuration.UploadRequestStoragePath)}");
    }
    if (!Directory.Exists($"{Path.Combine(builder.Environment.ContentRootPath, _configuration.BackupRequestStoragePath)}"))
    {
        Directory.CreateDirectory($"{Path.Combine(builder.Environment.ContentRootPath, _configuration.BackupRequestStoragePath)}");
    }
    try
    {
        await context.Database.MigrateAsync();
        await DbSeeder.MigrateAsync(context,loggerFactory);
       
    }
    catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex.Message);
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (_configuration.UseHttpsRedirection)
{
   app.UseHttpsRedirection();
}

if (_configuration.UseSpaServices && builder.Environment.EnvironmentName != Environments.Development)
{
    app.UseDefaultFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = context =>
        {
            if (context.File.Name.EndsWith(".js.gz"))
            {
                context.Context.Response.Headers[HeaderNames.ContentType] = "text/javascript";
                context.Context.Response.Headers[HeaderNames.ContentEncoding] = "gzip";
            }
            const int timeInSeconds = 60 * 60 * 24;
            context.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=" + timeInSeconds;
        }
    });
}

app.UseStaticFiles();

if (_configuration.UseSpaServices && builder.Environment.EnvironmentName != Environments.Development)
{
    app.UseSpaStaticFiles();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpLogging();

app.UseMiddleware<QueryToken>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//Static files protects


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, _configuration.UploadRequestStoragePath)),
    RequestPath = $"/{_configuration.UploadRequestStoragePath}",
    OnPrepareResponse = ctx =>
    {
        ProtectedFileOptions.CheckAuth(ctx, _configuration);
    },
    
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, _configuration.BackupRequestStoragePath)),
    RequestPath = $"/{_configuration.BackupRequestStoragePath}"
});
//SignalR

app.MapHub<TestHub>("/testHub");

//SignalR


app.UseCors("CorsPolicy");
app.MapControllers();

if (_configuration.UseSpaServices && builder.Environment.EnvironmentName != Environments.Development)
{
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = Path.Join(builder.Environment.ContentRootPath, "wwwroot");
    });
}

app.Run();
