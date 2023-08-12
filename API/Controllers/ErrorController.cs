using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    public class ErrorController : BaseApiController
    {
        [HttpGet]
        public IActionResult Geterror(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
