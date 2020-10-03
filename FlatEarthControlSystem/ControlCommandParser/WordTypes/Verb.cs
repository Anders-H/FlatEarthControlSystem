namespace FlatEarthControlSystem.ControlCommandParser.WordTypes
{
    public class Verb : WordType
    {
        public Verb(string stringRepresentation) : base(stringRepresentation)
        {
        }

        public static Verb Go() =>
            new Verb("GO");

        public static Verb Look() =>
            new Verb("LOOK");
    }
}