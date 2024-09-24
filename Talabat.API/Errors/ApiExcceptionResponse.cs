namespace Talabat.API.Errors
{
    public class ApiExcceptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExcceptionResponse(int StatusCode,string? message = null,string? details = null) : base(StatusCode,message)
        {
            this.Details = details;
        }
    }
}
