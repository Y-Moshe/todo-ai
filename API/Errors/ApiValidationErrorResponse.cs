namespace API.Errors;

public class ApiValidationErrorResponse : ApiErrorResponse
{
    public IEnumerable<string> Errors { get; set; }

    public ApiValidationErrorResponse() : base(400) { }
}
