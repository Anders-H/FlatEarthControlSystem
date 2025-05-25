namespace AdventureControlLibrary;

public enum ColorPalette
{
    Black = 0,
    White = 1,
    Red = 2,
    Cyan = 3,
    Purple = 4,
    Green = 5,
    Blue = 6,
    Yellow = 7,
    Orange = 8,
    Brown = 9,
    LightRed = 10,
    DarkGrey = 11,
    Grey = 12,
    LightGreen = 13,
    LightBlue = 14,
    LightGrey = 15
}

public static class ColorPaletteHelper
{
    public static Color GetColor(ColorPalette color) =>
        color switch
        {
            ColorPalette.Black => Color.FromArgb(0, 0, 0, 255),
            ColorPalette.White => Color.FromArgb(255, 255, 255, 255),
            ColorPalette.Red => Color.FromArgb(136, 0, 0, 255),
            ColorPalette.Cyan => Color.FromArgb(170, 255, 238, 255),
            ColorPalette.Purple => Color.FromArgb(204, 68, 204, 255),
            ColorPalette.Green => Color.FromArgb(0, 204, 85, 255),
            ColorPalette.Blue => Color.FromArgb(0, 0, 170, 255),
            ColorPalette.Yellow => Color.FromArgb(238, 238, 119, 255),
            ColorPalette.Orange => Color.FromArgb(221, 136, 85, 255),
            ColorPalette.Brown => Color.FromArgb(102, 68, 0, 255),
            ColorPalette.LightRed => Color.FromArgb(255, 119, 119, 255),
            ColorPalette.DarkGrey => Color.FromArgb(51, 51, 51, 255),
            ColorPalette.Grey => Color.FromArgb(119, 119, 119, 255),
            ColorPalette.LightGreen => Color.FromArgb(170, 255, 102, 255),
            ColorPalette.LightBlue => Color.FromArgb(0, 136, 255, 255),
            ColorPalette.LightGrey => Color.FromArgb(187, 187, 187, 255),
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
}