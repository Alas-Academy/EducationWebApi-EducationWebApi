using EducationWebApi.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationWebApi.DataAccess;
public static class ConfigureServices
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(connectionString));

        return services;
    }
}
