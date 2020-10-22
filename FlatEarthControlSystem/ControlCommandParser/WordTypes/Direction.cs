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
                {"north", "North"},
                {"n", "North"},
                {"ne", "North-East"},
                {"n-e", "North-East"},
                {"north east", "North-East"},
                {"north-east", "North-East"},
                {"northeast", "North-East"},
                {"east", "East"},
                {"e", "East"},
                {"se", "South-East"},
                {"s-e", "South-East"},
                {"south east", "South-East"},
                {"south-east", "South-East"},
                {"southeast", "South-East"},
                {"south", "South"},
                {"s", "South"},
                {"sw", "South-West"},
                {"s-w", "South-West"},
                {"south west", "South-West"},
                {"south-west", "South-West"},
                {"southwest", "South-West"},
                {"west", "West"},
                {"w", "West"},
                {"nw", "North-West"},
                {"n-w", "North-West"},
                {"north west", "North-West"},
                {"north-west", "North-West"},
                {"northwest", "North-West"}
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