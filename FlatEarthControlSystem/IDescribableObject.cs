namespace FlatEarthControlSystem
{
    public interface IDescribableObject
    {
        string OverviewText { get; }
        string DetailedViewText { get; }
    }
}