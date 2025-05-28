namespace AdventureControlLibrary.VectorGraphics;

public class Polygon
{
    public int LineNumber { get; }
    public ColorPalette Color { get; }
    public bool Filled { get; }
    public List<Point> Points { get; }

    public Polygon(int lineNumber, ColorPalette color, bool filled, params Point[]? points)
    {
        LineNumber = lineNumber;
        Color = color;
        Filled = filled;
        Points = [];

        if (points != null && points.Length > 0)
            Points.AddRange(points);
    }

    public void Draw(Graphics g)
    {
        if (Filled)
        {
            using var pen = new Pen(ColorPaletteHelper.GetColor(Color));
            g.DrawPolygon(pen, Points.ToArray());
        }
        else
        {
            using var brush = new SolidBrush(ColorPaletteHelper.GetColor(Color));
            g.FillPolygon(brush, Points.ToArray());
        }
    }
}