using TextAdventureGameInputParser;

namespace FlatEarthControlSystem
{
    public static class SentenceExtensions
    {
        public static bool Word1Is(this Sentence me, string other) =>
            me.Word1 != null && me.Word1.Value.Is(other);

        public static bool Word4IsEmpty(this Sentence me) =>
            me.Word4 == null || string.IsNullOrWhiteSpace(me.Word4.Value);
    }
}