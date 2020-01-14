using FlatEarthControlSystem.ControlCommandParser.Words;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class SuggestedCommand
    {
        public Verb Part1Verb { get; }
        public Noun Part2Noun { get; }
        public Preposition? Part3Preposition { get; }
        public Verb? Part4Verb { get; }
        
        internal SuggestedCommand(string verb, string noun)
        {
            Part1Verb = FlatEarth.Verbs.TryGet(verb);
            Part2Noun = new Noun(noun);
            Part3Preposition = null;
            Part4Verb = null;
        }

        internal SuggestedCommand(Verb verb, Noun noun)
        {
            Part1Verb = verb;
            Part2Noun = noun;
            Part3Preposition = null;
            Part4Verb = null;
        }

        internal SuggestedCommand(string verb, string noun, string preposition, string secondVerb)
        {
            Part1Verb = FlatEarth.Verbs.TryGet(verb);
            Part2Noun = new Noun(noun);
            Part3Preposition = new Preposition(preposition);
            Part4Verb = FlatEarth.Verbs.TryGet(secondVerb);
        }

        internal SuggestedCommand(Verb verb, Noun noun, Preposition preposition, Verb secondVerb)
        {
            Part1Verb = verb;
            Part2Noun = noun;
            Part3Preposition = preposition;
            Part4Verb = secondVerb;
        }
    }
}