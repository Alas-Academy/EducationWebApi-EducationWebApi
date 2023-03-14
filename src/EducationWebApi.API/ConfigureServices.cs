using EducationWebApi.Shared.Services;
using Newtonsoft.Json.Converters;

namespace EducationWebApi.API;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        })
        .AddViewLocalization()
        .AddDataAnnotationsLocalization();
        return services;
    }
}