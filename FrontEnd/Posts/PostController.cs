using FrontEnd.Posts.Query;
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
        public Task<Post[]> Get()
        {
            var grain = client.GetGrain<IFeedGrain>(0);            
            return grain.Query(new TagFeedQuery(Guid.NewGuid(), string.Empty));
        }

        [HttpGet("{id}")]
        public async Task<Post> Get(Guid id)
        {
            var grain = client.GetGrain<IPostGrain>(id);            
            return await grain.Get();
        }
        
        [HttpPost()]        
        [Route("{id?}")]
        public async Task<Post> Post([FromRoute] Guid? id, [FromBody] string post)
        {
            var userId = Guid.NewGuid();
            var grain = client.GetGrain<ICreatePostGrain>(0);            
            var result = await grain.Create(new SimpleTextRequest(post, userId) {  ReplyTo = id });

            return result;
        }
    }
}