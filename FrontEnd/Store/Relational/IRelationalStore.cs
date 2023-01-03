using GrainInterfaces.Model.Index;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd.Store.RelationalData
{
    public class RelationalStore : DbContext, IRelationalStore
    {
        public RelationalStore(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HashTagIndex> Tags { get; set; }
        public DbSet<PostIndex> Posts { get; set; }
    }

    public interface IRelationalStore
    {
        DbSet<HashTagIndex> Tags { get; set; }

        DbSet<PostIndex> Posts { get; set; }

        //DbSet<PostVisit> PostVisits { get; set; }
    }
}
