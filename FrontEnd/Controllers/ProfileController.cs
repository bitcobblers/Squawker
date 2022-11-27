using GrainInterfaces.Model;
using GrainInterfaces.State;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IClusterClient client;
        private readonly ILogger<ProfileController> logger;

        public ProfileController(IClusterClient client, ILogger<ProfileController> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Profile>> Get()
        {
            var grain = this.client.GetGrain<IProfileGrain>(Guid.NewGuid());
            var content = await grain.Get();
            return new[] { content };

        }
    }
}