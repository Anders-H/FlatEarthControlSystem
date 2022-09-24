using System.Collections.Generic;

namespace FlatEarthControlSystem.WorldDefinition;

public class Exit : IStatefulObject, IDescribableObject
{
    public string DirectionName { get; set; }
    public string TargetRoomId { get; set; }
    public List<string> AvailableStates { get; }
    public string UsefulState { get; set; }
    public string CurrentState { get; set; }
    public string OverviewText { get; set; }
    public string DetailedViewText { get; set; }
    public bool Discovered { get; set; }

    public Exit(string directionName, string targetRoomId) : this(directionName, targetRoomId, "", "", "", "")
    {
    }

    public Exit(string directionName, string targetRoomId, string usefulState, string currentState, string overviewText, string detailedViewText)
    {
        DirectionName = directionName;
        TargetRoomId = targetRoomId;
        UsefulState = usefulState;
        CurrentState = currentState;
        OverviewText = overviewText;
        DetailedViewText = detailedViewText;
        AvailableStates = new List<string>();
        Discovered = true;
    }

    public override string ToString() =>
        DirectionName;
}