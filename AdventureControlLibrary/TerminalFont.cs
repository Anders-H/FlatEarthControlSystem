using System.Collections;

namespace AdventureControlLibrary;

internal class TerminalFont : IEnumerable
{
    private readonly Dictionary<char, CharacterPixelMatrix> _data;

    public TerminalFont()
    {
        _data = new Dictionary<char, CharacterPixelMatrix>
        {
            {
                ' ', new CharacterPixelMatrix(
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,,"
                )
            },
            {
                ',', new CharacterPixelMatrix(
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,**,,,,"
                )
            },
            {
                '.', new CharacterPixelMatrix(
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,,,,,,"
                )
            },
            {
                '?', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",,,,,**," +
                    ",,,,**,," +
                    ",,,**,,," +
                    ",,,,,,,," +
                    ",,,**,,," +
                    ",,,,,,,,"
                )
            },
            {
                '-', new CharacterPixelMatrix(
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",******," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,," +
                    ",,,,,,,,"
                )
            },
            {
                '0', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,***," +
                    ",******," +
                    ",***,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                '1', new CharacterPixelMatrix(
                    ",,,**,,," +
                    ",,***,,," +
                    ",****,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",******," +
                    ",,,,,,,,"
                )
            },
            {
                '2', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",,,, **," +
                    ",,, **,," +
                    ",, **,,," +
                    ", **,,,," +
                    ",******," +
                    ",,,,,,,,"
                )
            },
            {
                '3', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",,,,,**," +
                    ",,,,**,," +
                    ",,,,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                '4', new CharacterPixelMatrix(
                    ",,,,***," +
                    ",,,****," +
                    ",,**,**," +
                    ",**,,**," +
                    ",******," +
                    ",,,,,**," +
                    ",,,,,**," +
                    ",,,,,,,,"
                )
            },
            {
                '5', new CharacterPixelMatrix(
                    ",******," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",*****,," +
                    ",,,,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                '6', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,,,," +
                    ",*****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                '7', new CharacterPixelMatrix(
                    ",******," +
                    ",,,,,**," +
                    ",,,,,**," +
                    ",,,,**,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,,,,,,"
                )
            },
            {
                '8', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                '9', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,*****," +
                    ",,,,,**," +
                    ",,,,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'A', new CharacterPixelMatrix(
                    ",,,**,,," +
                    ",,****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",******," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,,,,,,,"
                )
            },
            {
                'B', new CharacterPixelMatrix(
                    ",*****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",*****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",*****,," +
                    ",,,,,,,,"
                )
            },
            {
                'C', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'D', new CharacterPixelMatrix(
                    ",****,,," +
                    ",**,**,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,**,," +
                    ",****,,," +
                    ",,,,,,,,"
                )
            },
            {
                'E', new CharacterPixelMatrix(
                    ",******," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",****,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",******," +
                    ",,,,,,,,"
                )
            },
            {
                'F', new CharacterPixelMatrix(
                    ",******," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",****,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",,,,,,,,"
                )
            },
            {
                'G', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,,,," +
                    ",**,***," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'H', new CharacterPixelMatrix(
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",******," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,,,,,,,"
                )
            },
            {
                'I', new CharacterPixelMatrix(
                    ",,****,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'J', new CharacterPixelMatrix(
                    ",,,****," +
                    ",,,,,**," +
                    ",,,,,**," +
                    ",,,,,**," +
                    ",,,,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'K', new CharacterPixelMatrix(
                    ",**,,**," +
                    ",**,,**," +
                    ",**,**,," +
                    ",****,,," +
                    ",**,**,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,,,,,,,"
                )
            },
            {
                'L', new CharacterPixelMatrix(
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",******," +
                    ",,,,,,,,"
                )
            },
            {
                'M', new CharacterPixelMatrix(
                    ",**,,,**" +
                    ",***,***" +
                    ",*******" +
                    ",**,*,**" +
                    ",**,,,**" +
                    ",**,,,**" +
                    ",**,,,**" +
                    ",,,,,,,,"
                )
            },
            {
                'N', new CharacterPixelMatrix(
                    ",**,,**," +
                    ",***,**," +
                    ",******," +
                    ",**,***," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,,,,,,,"
                )
            },
            {
                'O', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'P', new CharacterPixelMatrix(
                    ",*****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",*****,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",**,,,,," +
                    ",,,,,,,,"
                )
            },
            {
                'Q', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,**,"
                )
            },
            {
                'R', new CharacterPixelMatrix(
                    ",*****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",*****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,,,,,,,"
                )
            },
            {
                'S', new CharacterPixelMatrix(
                    ",,****,," +
                    ",**,,**," +
                    ",**,,,,," +
                    ",,****,," +
                    ",,,,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'T', new CharacterPixelMatrix(
                    ",******," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,,,,,,"
                )
            },
            {
                'U', new CharacterPixelMatrix(
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,,,,,,"
                )
            },
            {
                'V', new CharacterPixelMatrix(
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,**,,," +
                    ",,,,,,,,"
                )
            },
            {
                'W', new CharacterPixelMatrix(
                    ",**,,,**" +
                    ",**,,,**" +
                    ",**,,,**" +
                    ",**,*,**" +
                    ",*******" +
                    ",***,***" +
                    ",**,,,**" +
                    ",,,,,,,,"
                )
            },
            {
                'X', new CharacterPixelMatrix(
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,**,,," +
                    ",,****,," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,,,,,,,"
                )
            },
            {
                'Y', new CharacterPixelMatrix(
                    ",**,,**," +
                    ",**,,**," +
                    ",**,,**," +
                    ",,****,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,**,,," +
                    ",,,,,,,,"
                )
            },
            {
                'Z', new CharacterPixelMatrix(
                    ",******," +
                    ",,,,,**," +
                    ",,,,**,," +
                    ",,'**,,," +
                    ",,**,,,," +
                    ",**,,,,," +
                    ",******," +
                    ",,,,,,,,"
                )
            }
        };
    }

    public IEnumerator<KeyValuePair<char, CharacterPixelMatrix>> GetEnumerator() =>
        _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        _data.GetEnumerator();

    public int Count =>
        _data.Count;

    public bool ContainsKey(char key) =>
        _data.ContainsKey(key);

#pragma warning disable CS8601 // Possible null reference assignment.
    public bool TryGetValue(char key, out CharacterPixelMatrix value) =>
        _data.TryGetValue(key, out value);
#pragma warning restore CS8601 // Possible null reference assignment.

    public CharacterPixelMatrix this[int index] =>
        _data.ElementAt(index).Value;

    public CharacterPixelMatrix this[char key] =>
        _data[key];

    public IEnumerable<char> Keys =>
        _data.Keys;

    public IEnumerable<CharacterPixelMatrix> Values =>
        _data.Values;
}