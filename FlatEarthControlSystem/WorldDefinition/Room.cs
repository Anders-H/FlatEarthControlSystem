using System.Linq;
using FlatEarthControlSystem.ControlCommandParser.WordTypes;

namespace FlatEarthControlSystem.WorldDefinition
{
    public class Room
    {
        internal World? Parent { get; set; }
        private ExitList Exits { get; }
        public string Id { get; }
        public int VisitCount { get; set; }
        public string FirstEntryDescription { get; set; }
        public string Description { get; set; }
        public int LookCount { get; set; }
        public string FirstLook { get; set; }
        public string Look { get; set; }
        
        public Room(string id)
        {
            Id = id;
            Exits = new ExitList();
        }

        public Exit CreateExit(string directionName, string targetRoomId)
        {
            var e = new Exit(directionName, targetRoomId);
            Exits.Add(e);
            return e;
        }

        public string GetDescription()
        {
            var uppercase = Parent?.Uppercase ?? false;
            
            var text = VisitCount <= 1
                ? string.IsNullOrWhiteSpace(FirstEntryDescription) ? Description : FirstEntryDescription
                : string.IsNullOrWhiteSpace(Description) ? FirstEntryDescription : Description;

            return uppercase
                ? text.ToUpper()
                : text;
        }

        public string GetLookText()
        {
            LookCount++;
            return LookCount <= 1
                ? string.IsNullOrWhiteSpace(FirstLook) ? Look : FirstLook
                : string.IsNullOrWhiteSpace(Look)
                    ? FirstLook
                    : Look;
        }

        public bool CanGo(Noun direction, out string targetRoomId)
        {
            var exit = Exits.FirstOrDefault(x => x.DirectionName == direction.StringRepresentation);
            if (exit == null)
            {
                targetRoomId = "";
                return false;
            }
            if (!exit.Discovered)
            {
                targetRoomId = exit.TargetRoomId;
                return false;
            }
            targetRoomId = exit.TargetRoomId ?? "";
            return true;
        }

        public void AddExit(Exit exit) =>
            Exits.Add(exit);

        public void AddExit(string directionName, string targetRoomId) =>
            Exits.Add(new Exit(directionName, targetRoomId));
        
        public Exit? GetDiscoveredExit(Direction directionName) =>
            Exits.GetDiscoveredExit(directionName);

        public Exit GetAnyExit(string directionName) =>
            Exits.GetAnyExit(directionName);

        public ExitList GetDiscoveredExits() =>
            new ExitList(Exits.Where(x => x.Discovered));

        public ExitList GetAllExits() =>
            Exits;

        public override string ToString() =>
            Id;
    }
}