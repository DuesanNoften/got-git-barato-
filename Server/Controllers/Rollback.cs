using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollBack : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }

        public string Post(RollBackJ rollbackJ)
        {
            return rollbackJ.commit;
        }
    }
}

public class RollBackJ
{
    public string commit { get; set; }
    public string document { get; set; }
}
