using FlatEarthControlSystem.ControlCommandParser.WordTypes;
using FlatEarthControlSystem.ControlCommandParser.WordTypes.Nouns;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class SuggestedCommand
    {
        public Verb Word01Verb { get; }

        public Filler? Word02Filler { get; }
        
        public Noun? Word03Noun { get; }
        
        public Preposition? Word04Preposition { get; }

        public Filler? Word05Filler { get; }

        public Noun? Word06Noun { get; }

        public SuggestedCommand(Verb word01Verb, Filler? word02Filler, Noun? word03Noun, Preposition? word04Preposition, Filler? word05Filler, Noun? word06Noun)
        {
            Word01Verb = word01Verb;
            Word02Filler = word02Filler;
            Word03Noun = word03Noun;
            Word04Preposition = word04Preposition;
            Word05Filler = word05Filler;
            Word06Noun = word06Noun;
        }

        public static SuggestedCommand Go(Direction direction) =>
            new SuggestedCommand(Verb.Go(), null, direction, null, null, null);
    }
}