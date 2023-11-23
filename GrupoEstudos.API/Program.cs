using AspNetCoreRateLimit;
using HealthChecks.UI.Client;
using Logzio.DotNet.NLog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NLog;
using NLog.Config;
using NLog.Web;
using GrupoEstudos.Infra.IoC;
using System.Net.Mime;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
ConfigurationManager Configuration = builder.Configuration;

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4300",
                                             "https://GrupoEstudosweb.inhaquites.com")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                      });
});
#endregion

#region LogzIo
var config = new LoggingConfiguration();

var logzioTarget = new LogzioTarget
{
    Name = "Logzio",
    Token = Configuration.GetValue<string>("LogzIO:Token"),
    LogzioType = "nlog",
    ListenerUrl = Configuration.GetValue<string>("LogzIO:ListenerUrl"),
    BufferSize = 100,
    BufferTimeout = TimeSpan.Parse("00:00:05"),
    RetriesMaxAttempts = 3,
    RetriesInterval = TimeSpan.Parse("00:00:02"),
    Debug = false
};

config.AddRule(NLog.LogLevel.Error, NLog.LogLevel.Fatal, logzioTarget);

LogManager.Configuration = config;

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();

#endregion

#region injecao de dependencias
builder.Services.AddInfrastructureAPI(builder.Environment ,Configuration);
#endregion

#region Token Jwt
builder.Services.AddInfrastructureJWT(Configuration);
#endregion

#region redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
#endregion

#region HealchCheck
builder.Services.AddHealthChecks();
#endregion

#region Ratelimit
builder.Services.AddRateLimitInMemoryCache();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
//builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "riAtendimento.Auth.API", Version = "v1" });
//});
builder.Services.AddInfrastructureSwagger();
#endregion



//=====================================================================================


var app = builder.Build();




// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrupoEstudos API v1"));
//}


#region HealchCheck
app.UseHealthChecks("/status",
    new HealthCheckOptions()
    {
        ResponseWriter = async (context, report) =>
        {
            var result = JsonConvert.SerializeObject(
                new
                {
                    currentTime = DateTime.Now.ToString("g"),
                    statusApplication = report.Status.ToString(),
                    healthChecks = report.Entries.Select(e => new
                    {
                        check = e.Key,
                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                    })
                });
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(result);
        }

    });

app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/monitor";
});
#endregion


app.UseIpRateLimiting();


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
