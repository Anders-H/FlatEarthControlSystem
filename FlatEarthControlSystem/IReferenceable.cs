namespace FlatEarthControlSystem
{
    public interface IReferenceable
    {
        string UniqueName { get; }
        string RelaxedName { get; }
    }
}