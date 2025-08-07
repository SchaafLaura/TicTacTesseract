using System.Numerics;

namespace TicTacTesseract;
using Raylib_cs;
using static Raylib_cs.Raylib;
public static class RectangleExtensions
{
    public static Rectangle Shrink(this Rectangle r, float shrinkBy)
    {
        return new Rectangle(r.X + shrinkBy, r.Y + shrinkBy, r.Width - 2 * shrinkBy, r.Height - 2 * shrinkBy);
    }

    public static Rectangle[] Split(this Rectangle r)
    {
        var ret = new Rectangle[9];
        var s = r.Size.X;
        var third = s / 3.0f;
        var sixth = s / 6.0f;
        
        for(var i = 0; i < 3; i++)
        for (var j = 0; j < 3; j++)
            ret[i + j * 3] = new Rectangle(r.X + i * third, r.Y + j * third, third, third);
        return ret;
    }

    public static bool Contains(this Rectangle r, Vector2 p)
    {
        return
            p.X > r.X &&
            p.X < r.X + r.Size.X &&
            p.Y > r.Y &&
            p.Y < r.Y + r.Size.Y;
    }
    
}