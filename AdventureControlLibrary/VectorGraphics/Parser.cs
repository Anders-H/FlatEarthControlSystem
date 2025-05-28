namespace AdventureControlLibrary.VectorGraphics;

public class Parser
{
    private readonly string _source;

    public Parser(string source)
    {
        _source = source;
    }

    public void Parse()
    {
        var rows = _source.Split([Environment.NewLine], StringSplitOptions.None);

    }
}