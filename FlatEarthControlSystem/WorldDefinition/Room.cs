using System.Linq;
using FlatEarthControlSystem.Extensions;
using TextAdventureGameInputParser.WordClass;

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

        public Room(string id) : this(id, "", "", "", "")
        {
        }

        public Room(string id, string firstEntryDescription, string description, string firstLook, string look)
        {
            Id = id;
            FirstEntryDescription = firstEntryDescription;
            Description = description;
            FirstLook = firstLook;
            Look = look;
            Exits = new ExitList();
        }

        public Exit CreateExit(string directionName, string targetRoomId)
        {
            var e = new Exit(directionName, targetRoomId);
            Exits.Add(e);
            return e;
        }

        public string GetDescription() =>
            (
                (
                    VisitCount <= 1
                    ? FirstEntryDescription.IsEmpty() ? Description : FirstEntryDescription
                    : Description.IsEmpty() ? FirstEntryDescription : Description
                )
                ?? ""
            ).ToUpper();

        public string GetLookText()
        {
            LookCount++;

            var result = LookCount <= 1
                ? FirstLook.IsEmpty() ? Look : FirstLook
                : Look.IsEmpty()
                    ? FirstLook
                    : Look;

            return (
                result.IsEmpty()
                    ? Description.IsEmpty() ? FirstEntryDescription : Description
                    : result
                ) ?? "";
        }

        public bool CanGo(Noun direction, out string targetRoomId)
        {
            var exit = Exits.FirstOrDefault(x => x.DirectionName == direction.ToString());
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

        public Exit? GetDiscoveredExit(Noun directionName) =>
            Exits.GetDiscoveredExit(directionName.Value);

        public Exit? GetAnyExit(string directionName) =>
            Exits.GetAnyExit(directionName);

        public Exit? GetAnyExit(Noun direction) =>
            Exits.GetAnyExit(direction.ToString());

        public ExitList GetDiscoveredExits() =>
            new(Exits.Where(x => x.Discovered));

        public ExitList GetAllExits() =>
            Exits;

        public override string ToString() =>
            Id;
    }
}