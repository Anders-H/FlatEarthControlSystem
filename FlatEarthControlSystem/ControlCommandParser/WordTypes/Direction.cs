using System.Collections.Generic;

namespace FlatEarthControlSystem.ControlCommandParser.WordTypes
{
    public class Direction : Noun
    {
        private static readonly Dictionary<string, string> _dictionary;

        static Direction()
        {
            _dictionary = new Dictionary<string, string>
            {
                {"north", "NORTH"},
                {"n", "NORTH"},
                {"ne", "NORTH-EAST"},
                {"n-e", "NORTH-EAST"},
                {"north east", "NORTH-EAST"},
                {"north-east", "NORTH-EAST"},
                {"northeast", "NORTH-EAST"},
                {"east", "east"},
                {"e", "east"},
                {"se", "SOUTH-EAST"},
                {"s-e", "SOUTH-EAST"},
                {"south east", "SOUTH-EAST"},
                {"south-east", "SOUTH-EAST"},
                {"southeast", "SOUTH-EAST"},
                {"south", "south"},
                {"s", "south"},
                {"sw", "SOUTH-WEST"},
                {"s-w", "SOUTH-WEST"},
                {"south west", "SOUTH-WEST"},
                {"south-west", "SOUTH-WEST"},
                {"southwest", "SOUTH-WEST"},
                {"west", "WEST"},
                {"w", "WEST"},
                {"nw", "NORTH-WEST"},
                {"n-w", "NORTH-WEST"},
                {"north west", "NORTH-WEST"},
                {"north-west", "NORTH-WEST"},
                {"northwest", "NORTH-WEST"}
            };
        }

        public Direction(string stringRepresentation) : base(stringRepresentation)
        {
        }

        public static Direction? FromNoun(string noun) =>
            !_dictionary.TryGetValue((noun ?? "").Trim().ToLower(), out var dir)
                ? null
                : new Direction(dir);

        public static Direction? FromNoun(Noun noun) =>
            FromNoun(noun.ToString());
    }
}