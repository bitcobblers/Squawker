using FrontEnd.Profiles;
using GrainInterfaces;
using GrainInterfaces.Model;
using GrainInterfaces.Posts;
using GrainInterfaces.States;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Posts
{

    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IClusterClient client;
        private readonly ILogger<PostController> logger;

        public PostController(IClusterClient client, ILogger<PostController> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<Post[]> Get()
        {
            var grain = client.GetGrain<IFeedGrain>(0);
            var userName = string.Empty;

            return await grain.Get(userName);
        }

        [HttpPost]
        public async Task<Post> Post([FromBody] string post)
        {
            var userId = Guid.NewGuid();
            var grain = client.GetGrain<ICreatePostGrain>(0);
            var result = await grain.Create(new SimpleTextRequest(post, userId));

            return result;
        }
    }
}