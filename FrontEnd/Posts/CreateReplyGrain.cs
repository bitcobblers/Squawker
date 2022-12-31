using GrainInterfaces.Posts;
using Orleans.Concurrency;

namespace Grains.Posts
{
    [StatelessWorker]
    public class CreateReplyGrain : Grain, ICreateReplyGrain
    {
        private readonly IClusterClient client;

        public CreateReplyGrain(IClusterClient client)
        {
            this.client = client;
        }
        public async Task ReplyTo(Guid? message, Guid reply)
        {
            if (message.HasValue) {

                var statisticsGrain = this.client.GetGrain<IPostStatisticsGrain>(message.Value);
                await statisticsGrain.Comment();
            }
            
            return;
        }
    }
}