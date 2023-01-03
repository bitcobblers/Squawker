using GrainInterfaces.Model.Index;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Store.RelationalData
{
    public interface IRelationalStore
    {
        DbSet<HashTagLink> HashTagLinks { get; set; }

        DbSet<PostIndex> PostsIndex { get; set; }
        
    }
}
