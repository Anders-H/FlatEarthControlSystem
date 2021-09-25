using TextAdventureGameInputParser;

namespace FlatEarthControlSystem
{
    public class SentenceWrapper
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Sentence Sentence { get; set; }
    }
}