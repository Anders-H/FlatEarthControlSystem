namespace FlatEarthControlSystem.ControlCommandParser.WordTypes
{
    public class Noun : WordType
    {
        public Noun(string stringRepresentation) : base(stringRepresentation)
        {
        }

        public static Noun North() =>
            new Noun("North");

        public static Noun NorthEast() =>
            new Noun("North-East");

        public static Noun East() =>
            new Noun("East");

        public static Noun SouthEast() =>
            new Noun("South-East");

        public static Noun South() =>
            new Noun("South");

        public static Noun SouthWest() =>
            new Noun("South-West");

        public static Noun West() =>
            new Noun("West");

        public static Noun NorthWest() =>
            new Noun("North-West");
    }
}