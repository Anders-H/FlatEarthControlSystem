using System.Collections.Generic;

namespace FlatEarthControlSystem.ControlCommandParser.WordTypes.Nouns
{
    public class Direction : Noun
    {
        private static readonly Dictionary<string, string> Dictionary;

        static Direction()
        {
            Dictionary = new Dictionary<string, string>
            {
                {"north", North().ToString()},
                {"n", North().ToString()},
                {"ne", NorthEast().ToString()},
                {"n-e", NorthEast().ToString()},
                {"north east", NorthEast().ToString()},
                {"north-east", NorthEast().ToString()},
                {"northeast", NorthEast().ToString()},
                {"east", East().ToString()},
                {"e", East().ToString()},
                {"se", SouthEast().ToString()},
                {"s-e", SouthEast().ToString()},
                {"south east", SouthEast().ToString()},
                {"south-east", SouthEast().ToString()},
                {"southeast", SouthEast().ToString()},
                {"south", South().ToString()},
                {"s", South().ToString()},
                {"sw", SouthWest().ToString()},
                {"s-w", SouthWest().ToString()},
                {"south west", SouthWest().ToString()},
                {"south-west", SouthWest().ToString()},
                {"southwest", SouthWest().ToString()},
                {"west", West().ToString()},
                {"w", West().ToString()},
                {"nw", NorthWest().ToString()},
                {"n-w", NorthWest().ToString()},
                {"north west", NorthWest().ToString()},
                {"north-west", NorthWest().ToString()},
                {"northwest", NorthWest().ToString()}
            };
        }

        public Direction(string stringRepresentation) : base(stringRepresentation)
        {
        }

        public static Direction? FromNoun(string noun) =>
            !Dictionary.TryGetValue((noun ?? "").Trim().ToLower(), out var dir)
                ? null
                : new Direction(dir);

        public static Direction? FromNoun(Noun noun) =>
            FromNoun(noun.ToString());
    }
}