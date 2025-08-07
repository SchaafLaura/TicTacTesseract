using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace TicTacTesseract;
internal class Button(Rectangle r, string label, Action onClick)
{
    private Rectangle r = r;
    private Action onClick = onClick;
    private string label = label;

    private const int fontSize = 30;
    
    public void Display()
    {
        var c = r.Contains(GetMousePosition()) ? White : Black;
        DrawRectangleLinesEx(r, 2, c);

        var w = MeasureText(label, fontSize);
        var sx = (r.Width - w) / 2;

        var sy = 15;
        
        DrawText(label, (int)(r.X + sx), (int)(r.Y + sy), fontSize, c);
    }

    public void TryClick()
    {
        if (!IsMouseButtonPressed(MouseButton.Left))
            return;
        if (!r.Contains(GetMousePosition()))
            return;
        onClick?.Invoke();
    }
}