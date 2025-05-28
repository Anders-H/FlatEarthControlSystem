using AdventureControlLibrary.VectorGraphics;

namespace AdventureControlLibrary;

public partial class GraphicsOutputControl : UserControl
{
    public const int PixelsWidth = 640;
    public const int PixelsHeight = 96;

    public GraphicsOutputControl()
    {
        InitializeComponent();
    }

    public void SetGraphics(string source)
    {
        var parser = new Parser(source);
        

    }
}