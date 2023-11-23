using Microsoft.AspNetCore.Mvc;

namespace net8test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        
        [HttpGet(Name = "test")]
        public async Task<IActionResult> Get()
        {
            return Ok("Net 8 ok");
        }
    }
}
