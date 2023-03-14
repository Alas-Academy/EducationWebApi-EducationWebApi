
using EducationWebApi.Application;
using EducationWebApi.DataAccess;
using EducationWebApi.DataAccess.Persistence;

namespace EducationWebApi.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDataAccessServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddWebUIServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();

            // Initialise and seed database
            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                initialiser.InitialiseAsync().Wait();
                initialiser.SeedAsync().Wait();
            }
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}