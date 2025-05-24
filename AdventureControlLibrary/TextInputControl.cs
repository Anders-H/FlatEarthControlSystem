using System.Drawing.Drawing2D;
using System.Text;
using TextAdventureGameInputParser;
using Matrix = CharacterMatrix.Matrix;

namespace AdventureControlLibrary;

public partial class TextInputControl : UserControl
{
    public event CommandEnteredDelegate? CommandEntered;
    private readonly TerminalFont _font;
    private readonly Bitmap _bitmap;
    private readonly Matrix _characterMatrix;
    private bool _inputEnabled = true;
    private bool _cursorVisible;
    public const int PixelsWidth = 640;
    public const int PixelsHeight = 104;
    public const int CharactersWidth = 80;
    public const int CharactersHeight = 13;
    public const int CharacterWidth = 8;
    public const int CharacterHeight = 8;
    public int CursorX { get; private set; }
    public const int CursorY = 12;
    public Parser Parser { get; } = new();

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
        g.CompositingQuality = CompositingQuality.HighSpeed;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;

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
        if (!_inputEnabled)
            return;

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
            case Keys.Home:
                CursorX = 0;
                ResetBlink();
                break;
            case Keys.End:
                GoToLastCharacter();
                break;
            case Keys.Back:
                if (CursorX == CharactersWidth - 1)
                {
                    _characterMatrix.SetAt(CharactersWidth - 2, CursorY, _characterMatrix.GetAt(CharactersWidth - 1, CursorY));
                    _characterMatrix.SetAt(CharactersWidth - 1, CursorY, ' ');
                    CursorX--;
                }
                else if (CursorX > 0 && CursorX < CharactersWidth - 1)
                {
                    for (var x = CursorX - 1; x < CharactersWidth - 1; x++)
                        _characterMatrix.SetAt(x, CursorY, _characterMatrix.GetAt(x + 1, CursorY));

                    _characterMatrix.SetAt(CharactersWidth - 1, CursorY, ' ');
                    CursorX--;
                }

                ResetBlink();
                break;
            case Keys.Delete:
                if (CursorX == CharactersWidth - 1)
                {
                    _characterMatrix.SetAt(CharactersWidth - 1, CursorY, ' ');
                }
                else if (CursorX == CharactersWidth - 2)
                {
                    _characterMatrix.SetAt(CharactersWidth - 2, CursorY, _characterMatrix.GetAt(CharactersWidth - 1, CursorY));
                    _characterMatrix.SetAt(CharactersWidth - 1, CursorY, ' ');
                }
                else if (CursorX >= 0 && CursorX < CharactersWidth - 2)
                {
                    for (var x = CursorX; x < CharactersWidth - 1; x++)
                        _characterMatrix.SetAt(x, CursorY, _characterMatrix.GetAt(x + 1, CursorY));

                    _characterMatrix.SetAt(CharactersWidth - 1, CursorY, ' ');
                }

                ResetBlink();
                break;
            case Keys.Enter:
                if (!HasInput(out var input))
                {
                    _characterMatrix.ScrollUp();
                    ResetBlink();
                    return;
                }

                var success = GetParsedInput(input, out var sentence, out var sentenceString);
                _characterMatrix.ScrollUp();
                CursorX = 0;

                if (sentenceString.Length > 0)
                {
                    for (var x = 0; x < CharactersWidth; x++)
                        _characterMatrix.SetAt(x, CursorY - 1, x < sentenceString.Length ? sentenceString[x] : ' ');
                }

                ResetBlink();
                Refresh();

                if (!success || sentence == null)
                    Write("I DON'T UNDERSTAND.");
                else
                    CommandEntered?.Invoke(this, sentence);

                break;
            case Keys.Insert:
                _characterMatrix.InsertAt(CursorX, CursorY);
                ResetBlink();
                break;
            case Keys.Oem2: // Apostrophe
            case Keys.Space:
            case Keys.Oemcomma:
            case Keys.OemPeriod:
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

    public void Write(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return;

        _inputEnabled = false;
        timer1.Enabled = false;
        _cursorVisible = false;
        Refresh();
        var wrappedText = WordWrap(text);

        foreach (var t in wrappedText)
        {
            if (t.Length > 0)
            {
                for (var x = 0; x < t.Length; x++)
                {
                    _characterMatrix.SetAt(x, CursorY, t[x]);
                    Thread.Yield();
                    Refresh();
                }

                _characterMatrix.ScrollUp();
            }
            else
            {
                Thread.Yield();
                _characterMatrix.ScrollUp();
                Refresh();
            }
        }

        timer1.Enabled = true;
        _inputEnabled = true;
        ResetBlink();
        WindowsInput.Buffer.FlushKeyboardBuffer(Handle);
    }

    private static List<string> WordWrap(string text)
    {
        if (string.IsNullOrEmpty(text))
            return [text];

        var words = text.Split(' ');
        var line = "";
        var result = new List<string>();

        foreach (var word in words)
        {
            if ((line + word).Length > CharactersWidth)
            {
                if (!string.IsNullOrEmpty(line))
                    result.Add(line.Trim());

                line = "";
            }
            line += word + " ";
        }

        if (!string.IsNullOrWhiteSpace(line))
            result.Add(line.TrimEnd());

        return result;
    }

    private bool HasInput(out string input)
    {
        var s = new StringBuilder();

        for (var x = 0; x < CharactersWidth; x++)
            s.Append(_characterMatrix.GetAt(x, CursorY));

        input = s.ToString().Trim();
        return !string.IsNullOrWhiteSpace(input);
    }

    private bool GetParsedInput(string entered, out Sentence? sentence, out string sentenceAsString)
    {
        var parsed = Parser.Parse(entered);
        
        if (parsed.ParseSuccess)
        {
            sentence = parsed;
            sentenceAsString = sentence.CleanInput;
            return true;
        }

        sentence = null;

        while (entered.IndexOf("  ", StringComparison.Ordinal) > -1)
            entered = entered.Replace("  ", " ");

        sentenceAsString = entered;
        return false;
    }

    private char GetCharacter(Keys key)
    {
        switch (key)
        {
            case Keys.Oemcomma:
                return ',';
            case Keys.OemPeriod:
                return '.';
            case Keys.Oem2:
                return '\'';
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

    private void GoToLastCharacter()
    {
        if (_characterMatrix.GetAt(CharactersWidth - 1, CursorY) != ' ')
        {
            CursorX = CharactersWidth - 1;
            ResetBlink();
            return;
        }

        for (var x = CharactersWidth - 2; x >= 0; x--)
        {
            if (_characterMatrix.GetAt(x, CursorY) != ' ')
            {
                CursorX = x + 1;
                ResetBlink();
                return;
            }
        }
    }

    private void TypeCharacter(char character)
    {
        _characterMatrix.InsertAt(CursorX, CursorY);
        _characterMatrix.SetAt(CursorX, CursorY, character);

        if (CursorX < CharactersWidth - 1)
            CursorX++;

        ResetBlink();
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