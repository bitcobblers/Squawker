using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace FrontEnd.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IClusterClient client;
        private readonly ILogger<ProfileController> logger;

        public PostController(IClusterClient client, ILogger<ProfileController> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            var grain = this.client.GetGrain<IPostGrain>(Guid.NewGuid());
            var content = await grain.GetContent();
            return new[] { content };
        }
    }
}