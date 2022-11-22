using GrainInterfaces;

namespace Grains
{
    public class AuthorGrain : Orleans.Grain<Author>, IAuthorGrain
    {
        public async Task<Author> Get()
        {
            return this.State;
        }
    }
}