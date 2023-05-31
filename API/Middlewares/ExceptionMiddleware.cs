using System.Text.Json;
using API.Errors;

namespace API.Middlewares;

public class ExceptionMiddleware
{
    public RequestDelegate Next { get; }
    public ILogger<ExceptionMiddleware> Logger { get; }
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _env = env;
        Logger = logger;
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = _env.IsDevelopment()
                ? new ApiException(500, ex.Message, ex.StackTrace.ToString())
                : new ApiException(500);

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
