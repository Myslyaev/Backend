using Backend.Core.Models.Devices;
using Backend.Core.Models.Users;

namespace Backend.API.Extensions;

public static class ConfigureServices
{
    public static void ConfigureApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(UsersMappingProfile), typeof(DevicesMappingProfile));
        services.ConfigureAuthentication();
        services.ConfigureDataBase();
        services.ConfigureSwagger();
    }
}
