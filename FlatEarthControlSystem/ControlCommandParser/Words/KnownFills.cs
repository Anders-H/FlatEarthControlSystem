using System.Collections.Generic;

namespace FlatEarthControlSystem.ControlCommandParser.Words
{
    public class KnownFills : List<KnownFill>
    {
        public KnownFills()
        {
            Add(new KnownFill("A"));
            Add(new KnownFill("THE"));
            Add(new KnownFill("AT"));
            Add(new KnownFill("TO"));
        }

        public bool IsKnownFill(KnownFill word) =>
            this.Exists(x => x.StringRepresentation == word.StringRepresentation);
    }
}