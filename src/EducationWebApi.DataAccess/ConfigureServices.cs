using EducationWebApi.Application.Common;
using EducationWebApi.DataAccess.Common.MediatRPublishStrategy;
using EducationWebApi.DataAccess.Persistence;
using EducationWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

        #region Identity services configure
        services.AddIdentity<ApplicationUser, AppRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
        #endregion

        services.AddScoped<IMediatorPublisher, MediatorPublisher>();

        return services;
    }
}
