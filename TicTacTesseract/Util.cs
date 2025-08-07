using Raylib_cs;
using static Raylib_cs.Raylib;

public static class Util
{
    public static void DrawTextureInverted(Texture2D texture, int x, int y)
    {
        DrawTexturePro(
            texture,
            new Rectangle(0, 0, texture.Width, -texture.Height),
            new Rectangle(0, 0, texture.Width, texture.Height),
            new(x, y), 0, Color.White);
    }
    
    public static int HashMove(IEnumerable<int> move)
    {
        var i = 0;
        return move.Aggregate(0, (acc, next) =>
        {
            acc += next * (int)Math.Pow(9, i);
            i++;
            return acc;
        });
    }

    public static (int, int) MoveToXY(int[] move)
    {
        var x = 0;
        var y = 0;
        for (var l = move.Length - 1; l > 0; l--)
        {
            var exponent = (move.Length - 1) - l;
            x += (move[l] % 3) * (int)Math.Pow(3, exponent);
            y += (move[l] / 3) * (int)Math.Pow(3, exponent);
        }
        return (x, y);
    }

    public static bool Contains3InARow(HashSet<(int, int)> moves)
    {
        foreach (var (x, y) in moves)
        {
            if ((Has(x + 1, y)     && Has(x + 2, y))     ||     // right
                (Has(x,     y + 1) && Has(x,     y + 2)) ||     // down
                (Has(x + 1, y + 1) && Has(x + 2, y + 2)) ||     // right-down
                (Has(x - 1, y + 1) && Has(x - 2, y + 2)))       // left-down
                return true;
        }
        return false;

        bool Has(int x, int y) => moves.Contains((x, y));
    }
}