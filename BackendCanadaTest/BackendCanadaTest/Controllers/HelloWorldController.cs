using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendCanadaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        // GET api/<HelloWorldController>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public string Get()
        {
            Response.StatusCode = StatusCodes.Status200OK;
            Response.ContentType = "text/plain";
            return "hello";
        }
    }
}
