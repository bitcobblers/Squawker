using FrontEnd.Posts.Query;
using FrontEnd.Profiles;
using GrainInterfaces.Posts;
using GrainInterfaces.States;
using GrainInterfaces.Tags;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Posts
{

    [ApiController]
    [Route("t")]
    public class HashTagsController : ControllerBase
    {
        private readonly IClusterClient client;
        private readonly ILogger<ProfileController> logger;

        public HashTagsController(IClusterClient client, ILogger<ProfileController> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{tag}")]
        public Task<Post[]> Get(string tag)
        {                        
            var grain = client.GetGrain<IFeedGrain>(0);
            return grain.Query(new TagFeedQuery(Guid.NewGuid(), tag));            
        }        
    }
}