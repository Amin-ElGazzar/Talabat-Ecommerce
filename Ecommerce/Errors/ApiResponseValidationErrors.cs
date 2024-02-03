namespace Ecommerce.Errors
{
    // bad request when model state is not valid
    public class ApiResponseValidationErrors:ApiResponse
    {
        public ICollection<string> Errors { get; set; }

        public ApiResponseValidationErrors():base(400)
        {
            Errors = new List<string>();
        }
    }
}
