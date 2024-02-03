namespace Ecommerce.Errors.middlewere_handel_exception
{
    public class ApiResponseException :ApiResponse
    {
        private readonly string? details;

        public ApiResponseException(int statusCode, string? message=null,string? details= null):base(statusCode,message)
        {
            this.details = details;
        }
    }
}
