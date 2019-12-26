using System.Collections.Generic;

namespace FlatEarthControlSystem.WorldDefinition
{
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

        public Exit() : this("", "")
        {
        }
            
        public Exit(string directionName, string targetRoomId)
        {
            DirectionName = directionName;
            TargetRoomId = targetRoomId;
            AvailableStates = new List<string>();
            Discovered = true;
        }

        public override string ToString() =>
            DirectionName;
    }
}