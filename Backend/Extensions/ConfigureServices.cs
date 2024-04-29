using Backend.Core.Models.Devices;
using Backend.Core.Models.Users;

namespace Backend.API.Extensions;

public static class ConfigureServices
{
    public static void ConfigureApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(UsersMappingProfile), typeof(DevicesMappingProfile));
    }
}
