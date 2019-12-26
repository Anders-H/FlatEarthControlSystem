using System.Collections.Generic;

namespace FlatEarthControlSystem
{
    public interface IStatefulObject
    {
        List<string> AvailableStates { get; }
        string UsefulState { get; }
        string CurrentState { get; }
    }
}