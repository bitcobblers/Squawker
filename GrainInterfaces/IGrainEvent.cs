namespace GrainInterfaces.Model
{
    public interface IGrainEvent<TType>
    {
        void Apply(TType state);
    }

}