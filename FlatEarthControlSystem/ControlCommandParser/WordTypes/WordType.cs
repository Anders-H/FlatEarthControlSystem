namespace FlatEarthControlSystem.ControlCommandParser.WordTypes
{
    public abstract class WordType
    {
        protected string StringRepresentation { get; }

        protected WordType(string stringRepresentation)
        {
            StringRepresentation = stringRepresentation;
        }
    }
}