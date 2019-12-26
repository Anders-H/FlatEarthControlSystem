using FlatEarthControlSystem.ControlCommandParser.Words;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class Command
    {
        private static readonly Verbs Verbs;
        public static readonly KnownFills KnownFills;
        public Intention Intention { get; }
        public Verb Part1Verb { get; }
        public Noun Part2Noun { get; }
        public Preposition? Part3Preposition { get; }
        public Verb? Part4Verb { get; }

        static Command()
        {
            Verbs = new Verbs();
            KnownFills = new KnownFills();
        }
        
        internal Command(Intention intention, string verb, string noun)
        {
            Intention = intention;
            Part1Verb = Verbs.TryGet(verb);
            Part2Noun = new Noun(noun);
            Part3Preposition = null;
            Part4Verb = null;
        }

        internal Command(Intention intention, Verb verb, Noun noun)
        {
            Intention = intention;
            Part1Verb = verb;
            Part2Noun = noun;
            Part3Preposition = null;
            Part4Verb = null;
        }

        internal Command(Intention intention, string verb, string noun, string preposition, string secondVerb)
        {
            Intention = intention;
            Part1Verb = Verbs.TryGet(verb);
            Part2Noun = new Noun(noun);
            Part3Preposition = new Preposition(preposition);
            Part4Verb = Verbs.TryGet(secondVerb);
        }

        internal Command(Intention intention, Verb verb, Noun noun, Preposition preposition, Verb secondVerb)
        {
            Intention = intention;
            Part1Verb = verb;
            Part2Noun = noun;
            Part3Preposition = preposition;
            Part4Verb = secondVerb;
        }
    }
}