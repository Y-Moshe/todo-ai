using Core.Interfaces;
using Infrastructure.AppDb;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;

namespace API.Extensions;

public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<ISubTaskService, SubTaskService>();

        services.AddSingleton<IChatGPTService, ChatGPTService>();

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySQL(config.GetConnectionString("DefaultConnection")));

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        return services;
    }
}
