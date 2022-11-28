using GrainInterfaces;
using GrainInterfaces.Model;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<Post[]> Get()
        {
            var userName = string.Empty;
            var grain = this.client.GetGrain<IFeedGrain>(0);
            
            return await grain.Get(userName);            
        }
    }
}