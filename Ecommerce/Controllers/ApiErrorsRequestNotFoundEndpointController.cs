using Ecommerce.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ApiErrorsRequestNotFoundEndpointController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
