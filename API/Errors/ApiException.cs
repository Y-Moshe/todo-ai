namespace API.Errors;

public class ApiException : ApiErrorResponse
{
    public string Details { get; set; }

    public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
    {
        Details = details;
    }
}
