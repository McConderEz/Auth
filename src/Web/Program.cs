using Accounts.Infrastructure.Seeding;
using Framework.Middlewares;
using Serilog;
using Web;
using Web.Extensions;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddLogger(builder.Configuration);
builder.Services.AddAppMetrics();

builder.Services.AddHttpLogging(o =>
{
    o.CombineLogs = true;
});


builder.Services.AddSerilog();

builder.Services.AddModules(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

var app = builder.Build();

var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();

await accountsSeeder.SeedAsync();

app.UseExceptionMiddleware();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment() | app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseOpenTelemetryPrometheusScrapingEndpoint();
}

app.UseCors(builder =>
{
    builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseScopeDataMiddleware();
app.UseAuthorization();

app.MapControllers();


app.Run();