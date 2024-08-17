using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Staff.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class test : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            return null;
        }
    }
}
