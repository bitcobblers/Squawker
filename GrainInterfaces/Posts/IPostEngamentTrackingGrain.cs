using GrainInterfaces.States;

namespace GrainInterfaces.Posts
{
    public interface IReaction 
    { 
        string Categorty { get; set; }
        int Value { get; set; }    
    }

    public interface IPostStatisticsGrain : IGrainWithGuidKey
    {
        Task View();

        Task Comment();

        Task React(IReaction reaction);        

        Task<Statistics> Get();
    }
}