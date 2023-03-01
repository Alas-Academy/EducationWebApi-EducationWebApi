using EducationWebApi.API.Services;
using EducationWebApi.Application.Common;
using EducationWebApi.DataAccess.Common.MediatRPublishStrategy;
using EducationWebApi.Shared.Services;
using EducationWebApi.Shared.Services.Impl;

namespace EducationWebApi.API;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        return services;
    }
}