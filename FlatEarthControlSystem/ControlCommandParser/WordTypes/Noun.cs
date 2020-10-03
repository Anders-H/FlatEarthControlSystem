namespace FlatEarthControlSystem.ControlCommandParser.WordTypes
{
    public class Noun : WordType
    {
        public Noun(string stringRepresentation) : base(stringRepresentation)
        {
        }

        public static Noun North() =>
            new Noun("NORTH");
    }
}