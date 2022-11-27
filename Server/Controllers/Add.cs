using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class Add : ControllerBase
    {
        [HttpGet("Add")]
        public string Get()
        {
            return "g";
        }
    }
}
