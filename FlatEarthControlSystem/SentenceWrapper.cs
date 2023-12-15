using TextAdventureGameInputParser;

namespace FlatEarthControlSystem;

public class SentenceWrapper
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Sentence Sentence { get; set; }

    public SentenceWrapper(Sentence sentence) : this(false, "", sentence)
    {
    }

    public SentenceWrapper(bool success, string message, Sentence sentence)
    {
        Success = success;
        Message = message;
        Sentence = sentence;
    }
}