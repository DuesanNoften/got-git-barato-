using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class Commit : ControllerBase
    {
        [HttpGet("Commit/{id}")]
        public string Get(string id)
        {
            return id;
        }
    }
}
