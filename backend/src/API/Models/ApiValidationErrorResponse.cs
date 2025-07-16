namespace backend.src.API.Models;

public class ApiValidationErrorResponse : ApiResponse
{
    public ApiValidationErrorResponse(IEnumerable<string> errors) : base(400)
    {
        ValidationErrors = errors;
    }

    public IEnumerable<string> ValidationErrors { get; set; }
}
