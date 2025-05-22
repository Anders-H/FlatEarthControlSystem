using System.Drawing.Drawing2D;
using Matrix = CharacterMatrix.Matrix;

namespace AdventureControlLibrary;

public partial class TextInputControl : UserControl
{
    public event CommandEnteredDelegate CommandEntered;
    private readonly TerminalFont _font;
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
        _font = new TerminalFont();
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
                var c = _characterMatrix.GetAt(characterX, characterY);
                _ = _font.TryGetValue(c, out var matrix);
                var color = InputColor;

                if (cursorVisible)
                {
                    _bitmap.SetPixel(pixelX, pixelY, matrix.Pixels[x, y] ? BackgroundColor : color);
                }
                else
                {
                    _bitmap.SetPixel(pixelX, pixelY, matrix.Pixels[x, y] ? color : BackgroundColor);
                }

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
            case Keys.Back:
                // TODO!!!!
                break;
            case Keys.Delete:
                // TODO!!!!
                break;
            case Keys.Enter:
                // TODO!!!!
                break;
            case Keys.Insert:
                DoInsert();
                break;
            case Keys.Space:
            case Keys.Oemcomma:
            case Keys.OemPeriod:
            case Keys.OemQuestion:
            case Keys.OemMinus:
            case Keys.D0:
            case Keys.D1:
            case Keys.D2:
            case Keys.D3:
            case Keys.D4:
            case Keys.D5:
            case Keys.D6:
            case Keys.D7:
            case Keys.D8:
            case Keys.D9:
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
        switch (key)
        {
            case Keys.Oemcomma:
                return ',';
            case Keys.OemPeriod:
                return '.';
            case Keys.OemQuestion:
                return '?';
            case Keys.OemMinus:
                return '-';
            case Keys.D0:
                return '0';
            case Keys.D1:
                return '1';
            case Keys.D2:
                return '2';
            case Keys.D3:
                return '3';
            case Keys.D4:
                return '4';
            case Keys.D5:
                return '5';
            case Keys.D6:
                return '6';
            case Keys.D7:
                return '7';
            case Keys.D8:
                return '8';
            case Keys.D9:
                return '9';
            case Keys.A:
                return 'A';
            case Keys.B:
                return 'B';
            case Keys.C:
                return 'C';
            case Keys.D:
                return 'D';
            case Keys.E:
                return 'E';
            case Keys.F:
                return 'F';
            case Keys.G:
                return 'G';
            case Keys.H:
                return 'H';
            case Keys.I:
                return 'I';
            case Keys.J:
                return 'J';
            case Keys.K:
                return 'K';
            case Keys.L:
                return 'L';
            case Keys.M:
                return 'M';
            case Keys.N:
                return 'N';
            case Keys.O:
                return 'O';
            case Keys.P:
                return 'P';
            case Keys.Q:
                return 'Q';
            case Keys.R:
                return 'R';
            case Keys.S:
                return 'S';
            case Keys.T:
                return 'T';
            case Keys.U:
                return 'U';
            case Keys.V:
                return 'V';
            case Keys.W:
                return 'W';
            case Keys.X:
                return 'X';
            case Keys.Y:
                return 'Y';
            case Keys.Z:
                return 'Z';
            default:
                return ' ';
        }
    }

    private void TypeCharacter(char character)
    {
        DoInsert();
        _characterMatrix.SetAt(CursorX, CursorY, character);

        if (CursorX < CharactersWidth - 1)
            CursorX++;

        ResetBlink();
    }

    private void DoInsert()
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