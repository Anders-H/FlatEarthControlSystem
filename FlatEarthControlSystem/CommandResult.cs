namespace FlatEarthControlSystem;

public class CommandResult
{
    public bool Success { get; }
    public string Text { get; }

    public CommandResult(bool success, string text)
    {
        Success = success;
        Text = text;
    }
}