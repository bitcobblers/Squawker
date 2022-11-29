namespace Grains.DocumentData
{
    public interface IDocumentStore<TType> where TType : class
    {
        TType Get(Guid id);
        TType[] Get(Guid[] ids);
    }
}
