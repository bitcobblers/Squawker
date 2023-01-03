using GrainInterfaces.Model.Index;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Store.RelationalData
{
    public interface IRelationalStore
    {
        DbSet<HashTagIndex> Tags { get; set; }

        DbSet<PostIndex> Posts { get; set; }

        //DbSet<PostVisit> PostVisits { get; set; }
    }
}
