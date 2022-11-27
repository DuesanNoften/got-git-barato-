using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Reset : ControllerBase
    {
        [HttpGet("Reset/{id}")]
        public string Get(string id)
        {
            return id;
        }
    }
}
