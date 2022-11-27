using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sync : ControllerBase
    {
        [HttpGet("Sync")]
        public string Get()
        {
            return "g";
        }

        public string Post(RollBackJ rollbackJ)
        {
            return rollbackJ.commit;
        }
    }
}
