namespace FlatEarthControlSystem.ControlCommandParser.Words
{
    public abstract class Word
    {
        public string StringRepresentation { get; }
        
        internal Word(string stringRepresentation)
        {
            StringRepresentation = stringRepresentation;
        }

        public override string ToString() =>
            StringRepresentation;
    }
}