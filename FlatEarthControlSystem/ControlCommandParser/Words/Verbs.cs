using System.Collections.Generic;
using System.Linq;

namespace FlatEarthControlSystem.ControlCommandParser.Words
{
    public class Verbs : List<Verb>
    {
        public Verbs()
        {
            Add(new Verb("GO"));
            Add(new Verb("EXAMINE")); //TODO
            Add(new Verb("LOOK")); //TODO
            Add(new Verb("TAKE")); //TODO
            Add(new Verb("DROP")); //TODO
            Add(new Verb("USE")); //TODO
            Add(new Verb("OPEN")); //TODO
            Add(new Verb("CLOSE")); //TODO
            Add(new Verb("GIVE")); //TODO
            Add(new Verb("SHOW")); //TODO
        }

        public Verb? TryGet(string source) =>
            this.FirstOrDefault(x => x.StringRepresentation == source);
    }
}