using EducationWebApi.Application.Services.Impl.Stroage;
using EducationWebApi.Application.Services.Storage;
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
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<IStorageService, StorageService>();

        return services;
    }

    #region  Generic extension method for IServiceCollection
    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    #endregion
}