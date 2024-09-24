
namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }


        public ApiResponse(int StatusCode, string? Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message ?? GetMessageAccordingToStatusCode(StatusCode);
        }

        private string? GetMessageAccordingToStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You Are not Authorized",
                404 => "Not Found",
                500 => "Internal Server Error"
            };

        }
    }
}
