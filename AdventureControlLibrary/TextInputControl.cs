using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using Matrix = CharacterMatrix.Matrix;

namespace AdventureControlLibrary;

public partial class TextInputControl : UserControl
{
    public event CommandEnteredDelegate CommandEntered;
    private readonly Bitmap _bitmap;
    private readonly Matrix _characterMatrix;
    private bool _cursorVisible;
    public const int PixelsWidth = 650;
    public const int PixelsHeight = 104;
    public const int CharactersWidth = 80;
    public const int CharactersHeight = 13;
    public const int CharacterWidth = 8;
    public const int CharacterHeight = 8;
    public int CursorX { get; private set; }
    public const int CursorY = 12;

    public TextInputControl()
    {
        CursorX = 0;
        _bitmap = new Bitmap(PixelsWidth, PixelsHeight);
        _characterMatrix = new Matrix(CharactersWidth, CharactersHeight);
        _cursorVisible = false;
        InitializeComponent();
    }

    public Color BackgroundColor =>
        Color.Black;

    public Color InputColor =>
        Color.Chartreuse;

    public Color OutputColor =>
        Color.DarkTurquoise;

    private void TextInputControl_Resize(object sender, EventArgs e)
    {
        Invalidate();
    }

    private void TextInputControl_Paint(object sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        for (var y = 0; y < CharactersHeight; y++)
            for (var x = 0; x < CharactersWidth; x++)
                DrawCharacter(x, y, _cursorVisible && y == CursorY && x == CursorX);

        e.Graphics.DrawImage(_bitmap, 0, 0, Width, Height);
    }

    private void DrawCharacter(int characterX, int characterY, bool cursorVisible)
    {
        for (var y = 0; y < CharacterHeight; y++)
        {
            for (var x = 0; x < CharacterWidth; x++)
            {
                var pixelX = characterX * CharacterWidth + x;
                var pixelY = characterY * CharacterHeight + y;
                _bitmap.SetPixel(pixelX, pixelY, cursorVisible ? InputColor : BackgroundColor);
            }
        }
    }

    public void TellCursorActivity(Keys key)
    {
        switch (key)
        {
            case Keys.Left:
                if (CursorX > 0)
                    CursorX--;

                ResetBlink();
                break;
            case Keys.Right:
                if (CursorX < CharactersWidth - 1)
                    CursorX++;

                ResetBlink();
                break;
            case Keys.A:
            case Keys.B:
            case Keys.C:
            case Keys.D:
            case Keys.E:
            case Keys.F:
            case Keys.G:
            case Keys.H:
            case Keys.I:
            case Keys.J:
            case Keys.K:
            case Keys.L:
            case Keys.M:
            case Keys.N:
            case Keys.O:
            case Keys.P:
            case Keys.Q:
            case Keys.R:
            case Keys.S:
            case Keys.T:
            case Keys.U:
            case Keys.V:
            case Keys.W:
            case Keys.X:
            case Keys.Y:
            case Keys.Z:
                TypeCharacter(GetCharacter(key));
                break;
        }
    }

    private char GetCharacter(Keys key)
    {

    }

    private void TypeCharacter(char character)
    {

    }

    private void ResetBlink()
    {
        timer1.Enabled = false;
        _cursorVisible = true;
        timer1.Enabled = true;
        Invalidate();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        _cursorVisible = !_cursorVisible;
        Invalidate();
    }

    private void TextInputControl_Load(object sender, EventArgs e)
    {
        timer1.Enabled = true;
    }
}