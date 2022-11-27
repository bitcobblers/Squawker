using GrainInterfaces;
using GrainInterfaces.Model;
using GrainInterfaces.State;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace FrontEnd.Controllers
{
     
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<Post[]> Get()
        {
            var grain = this.client.GetGrain<IFeedGrain>(0);
            var result = await grain.Get(string.Empty);
            return result;
        }
    }
}