using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        [HttpGet("NotFound")]
        public IActionResult notfound()
        {
            var instance = Activator.CreateInstance(typeof(ApiResponse), new object[] { 404 , "some message" });
       
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("BadRequest")]
        public IActionResult BadRequester()
        {
            return BadRequest(new ApiResponse(401));
        }

        [HttpGet("ServerError")]
        public IActionResult ServerError()
        {
            string s = null;
            var x = s.Length;
            return Ok();
        }
    }
}
