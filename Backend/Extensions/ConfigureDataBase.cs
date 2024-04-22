using Backend.DAL;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Extensions;

public static class DataBaseExtensions
{
    public static void ConfigureDataBase(this IServiceCollection services)
    {
        services.AddDbContext<MamkinMainerContext>(
            options => options.UseNpgsql((Environment.GetEnvironmentVariable("MainerMysl")))
                              .UseSnakeCaseNamingConvention()
        );
    }
}

