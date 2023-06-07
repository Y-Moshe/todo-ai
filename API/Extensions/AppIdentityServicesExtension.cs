using System.Text;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class AppIdentityServicesExtension
{
    public static void AddAppIdentityServices(
        this IServiceCollection services,
        IConfiguration config,
        bool IsDevelopment)
    {
        services.AddScoped<IAccountService, AccountService>();

        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            string connectionString = IsDevelopment
                ? config.GetConnectionString("IdentityConnection")
                : config.GetValue<string>("IdentityConnection");

            options.UseMySQL(connectionString);
        });

        services.AddIdentityCore<AppUser>(options =>
        {
            // Password validation require:
            // - minLength of 6 must contain digits, lower and upper case.
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
        })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization();
    }
}
