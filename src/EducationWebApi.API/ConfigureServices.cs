using EducationWebApi.API.Filters;
using EducationWebApi.Shared.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;

namespace EducationWebApi.API;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddControllers(options => 
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        })
        .AddViewLocalization()
        .AddDataAnnotationsLocalization();
        return services;
    }
}