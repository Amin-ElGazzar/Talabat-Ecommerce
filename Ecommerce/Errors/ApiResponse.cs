
namespace Ecommerce.Errors
{
    // status code error
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public ApiResponse(int statusCode,string? errorMessage=null)
        {
            statusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultMessageForStatusCode( statusCode);
        }

        private string? GetDefaultMessageForStatusCode( int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401=> "Authorized, you are not",
                404 => "Resource was not found",
                500 => "Internal server error",
                _=>null,
            };
        }
    }
}
