using EducationWebApi.DataAccess.Common;
using EducationWebApi.DataAccess.Common.Impl.MediatRPublishStrategy;
using EducationWebApi.DataAccess.Persistence;
using EducationWebApi.DataAccess.Persistence.Interceptors;
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

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(connectionString));

        services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        #region Identity services configure
        services.AddIdentity<ApplicationUser, AppRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
        #endregion

        #region Dependency MediatorPublisher
        services.AddScoped<IMediatorPublisher, MediatorPublisher>();
        #endregion


        return services;
    }
}
