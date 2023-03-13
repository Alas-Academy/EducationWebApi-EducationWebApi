using EducationWebApi.Shared.Services;
using EducationWebApi.Shared.Services.Impl;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EducationWebApi.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<IDateTime, DateTimeService>();
        return services;
    }
}