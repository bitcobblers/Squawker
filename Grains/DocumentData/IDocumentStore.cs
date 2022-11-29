using GrainInterfaces.Model;

namespace Grains.DocumentData
{
    public interface IFileName
    {
        string Get<TType>(Guid id)
            where TType : class;
    }

    public interface IDocumentStore 
    {
        void Put<TType>(Guid id, TType model)
            where TType : class;

        TType? Get<TType>(Guid id)
            where TType : class;

        TType[] Get<TType>(Guid[] ids)
            where TType : class;
    }
}
