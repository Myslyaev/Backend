using Backend.API.Configuration;
using Backend.API.Extensions;
using Backend.BLL;
using Backend.DAL;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateBootstrapLogger();

    // Add services to the container.
    builder.Services.ConfigureApiServices();
    builder.Services.ConfigureBllServices();
    builder.Services.ConfigureDalServices();
    
    builder.Host.UseSerilog();

    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    Log.Information("Running app.");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("App stoped.");
    Log.CloseAndFlush();
}