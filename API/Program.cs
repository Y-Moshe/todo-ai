using API.Extensions;
using API.Middlewares;
using Infrastructure.AppDb;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAppServices(
    builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddAppIdentityServices(
    builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hi from .NET WebAPI!");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var appContext = services.GetRequiredService<AppDbContext>();
var appIdentityContext = services.GetRequiredService<AppIdentityDbContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await appContext.Database.MigrateAsync();
    await appIdentityContext.Database.MigrateAsync();
    logger.LogInformation("Migration completed successfully");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration!");
}

app.Run();
