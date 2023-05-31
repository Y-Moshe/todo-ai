namespace API.Errors;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ApiErrorResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    private string GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 => "Something went wrong! try again later",
            _ => "Something went wrong! try again later"
        };
    }
}
