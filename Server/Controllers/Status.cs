using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class Status : ControllerBase
    {
        [HttpGet("Status")]
        public string Get()
        {
            return "g";
        }
    }
}
