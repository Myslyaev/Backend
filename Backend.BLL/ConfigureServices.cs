using Backend.BLL.IServices;
using Backend.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.BLL;

public static class ConfigureServices
{
    public static void ConfigureBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDevicesService, DevicesService>();
    }
}
