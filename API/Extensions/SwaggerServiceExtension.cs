using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class SwaggerServiceExtension
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(config =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authentication Bearer scheme",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };

            var securityRequirement = new OpenApiSecurityRequirement
          {
            {
                securityScheme, new [] { "Bearer" }
            }
          };

            config.AddSecurityDefinition("Bearer", securityScheme);
            config.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}
