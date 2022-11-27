using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Commit : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }

        public string Post(CommitJ commitJ)
        {
            return commitJ.commit;
        }
    }
}

public class CommitJ
{
    public string commit { get; set; }

    public string document { get; set; }
}
