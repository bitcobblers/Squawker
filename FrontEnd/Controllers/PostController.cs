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

        [HttpPost]
        public async Task<Post> Post([FromBody]string post)
        {
            var grain = this.client.GetGrain<ICreatePostGrain>(0);
            var tagsGrain = this.client.GetGrain<ICreateHashTagsGrain>(0);

            var result = await grain.Create(new Post() { Id=Guid.NewGuid(), Content = post }, Guid.NewGuid());
            await tagsGrain.Create(result);
            return result;
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