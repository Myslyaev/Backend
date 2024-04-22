using Backend.DAL.IRepositories;
using Backend.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.DAL;

public static class ConfigureServices
{
    public static void ConfigureDalServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IDevicesRepository, DevicesRepository>();
    }
}
