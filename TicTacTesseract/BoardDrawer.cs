using System.Numerics;
using System.Runtime.InteropServices;

namespace TicTacTesseract;
using Raylib_cs;
using static Raylib_cs.Raylib;

public static class BoardDrawer
{
    public static RenderTexture2D BoardBackground { get; private set; }
    public static RenderTexture2D HighlightOverlay { get; private set; }
    public static RenderTexture2D PlayerMoves { get; private set; }
    public static RenderTexture2D MovePreview { get; private set; }
    
    public static int Depth { get; private set; }
    public static Rectangle Bounds { get; private set; }
    public static Color[] Colors { get; private set; }
    
    private static List<List<Rectangle>> boardsBounds = [];
    
    private static Color playerA = new Color(1.0f, 0.0f, 0.0f);
    private static Color playerB = new Color(0.2f, 0.2f, 1.0f);
    
    public static void Setup(Rectangle bounds, int depth, Color[] colors)
    {
        Depth = depth;
        Bounds = bounds;
        Colors = colors;
        InitBoardsBounds();
        InitTextures();
    }

    private static void InitBoardsBounds()
    {
        boardsBounds.Add([Bounds]);
        for (var i = 0; i < Depth + 1; i++)
        {
            boardsBounds.Add([]);
            foreach (var r in boardsBounds[i])
                boardsBounds[i + 1].AddRange(r.Split());
        }
    }

    private static void InitTextures()
    {
        BoardBackground = LoadRenderTexture((int) Bounds.Size.X, (int) Bounds.Size.Y);
        HighlightOverlay = LoadRenderTexture((int) Bounds.Size.X, (int) Bounds.Size.Y);
        PlayerMoves = LoadRenderTexture((int) Bounds.Size.X, (int) Bounds.Size.Y);
        MovePreview = LoadRenderTexture((int) Bounds.Size.X, (int) Bounds.Size.Y);
        
        BeginTextureMode(BoardBackground);
        //ClearBackground(new Color(23, 23, 27));
        ClearBackground(Color.White);
        for (var i = Depth; i >= 0; i--)
            foreach (var r in boardsBounds[i])
                Draw3x3(r.Shrink(3), Colors[Depth - i], (Depth-i)*3+3);
        EndTextureMode();
    }
    
    public static void DrawInitialMove(List<int> move)
    {
        BeginTextureMode(PlayerMoves);
        DrawX(GetBoard(move.ToArray()), playerA);
        EndTextureMode();
    }

    public static void PreviewMove(List<int> game, int toMove)
    {
        var move = game.Take(^(Depth + 1)..).Append(toMove).ToArray();
        BeginTextureMode(MovePreview);
        ClearBackground(Color.Blank);
        if (game.Count % 2 != Depth % 2)
            DrawX(GetBoard(move), playerA);
        else
            DrawO(GetBoard(move), playerB);
        EndTextureMode();
    }

    public static void ClearPreview()
    {
        BeginTextureMode(MovePreview);
        ClearBackground(Color.Blank);
        EndTextureMode();
    }

    public static void ClearHighlight()
    {
        BeginTextureMode(HighlightOverlay);
        ClearBackground(Color.Blank);
        EndTextureMode();
    }
    
    public static void DrawNewestMove(List<int> game)
    {
        BeginTextureMode(PlayerMoves);
        var b = game.Take(^(Depth+2) ..).ToArray();
        if (game.Count % 2 != Depth % 2)
            DrawO(GetBoard(b), playerB);
        else
            DrawX(GetBoard(b), playerA);
        EndTextureMode();
    }
    
    public static void Highlight(params int[][] boardIndex)
    {
        BeginTextureMode(HighlightOverlay);
        ClearBackground(new Color(0f, 0f, 0f, 0f));
        foreach (var i in boardIndex)
        {
            var b = GetBoard(i);
            DrawRectangleLinesEx(b, 4, Color.Magenta);
        }
        EndTextureMode();
    }

    public static int[] PixelPositionToBoardIndex(Vector2 pos)
    {
        List<int> index = [0];
        for (var i = 1; i < boardsBounds.Count; i++)
        {
            var prevIndex = 0;
            for (var k = 0; k < index.Count; k++)
                prevIndex += index[k] * (int)Math.Pow(9, index.Count - k);
            
            for (var j = prevIndex; j < prevIndex + 9; j++)
            {
                if (boardsBounds[i][j].Contains(pos))
                {
                    index.Add(j - prevIndex);
                    break;
                }
                if (j == prevIndex + 9 - 1)
                    return index.ToArray();
            }
        }
        return index.ToArray();
    }
    
    public static Rectangle GetBoard(int[] boardIndex)
    {
        if (boardIndex.Length == 0)
            return new Rectangle(0, 0, 0, 0);
        var listIndex = boardIndex.Length - 1;
        var index = 0;
        for (var i = listIndex; i > 0; i--)
            index += boardIndex[i] * (int)Math.Pow(9, listIndex - i);
        return boardsBounds[listIndex][index];
    }

    private static void DrawX(Rectangle r, Color col)
    {
        r = r.Shrink(5);
        DrawLineEx(
            startPos:   new(r.X, r.Y),
            endPos:     new(r.X + r.Size.X, r.Y + r.Size.Y),
            thick:      5,
            color:      col);
        DrawLineEx(
            startPos:   new(r.X, r.Y + r.Size.Y),
            endPos:     new(r.X + r.Size.X, r.Y),
            thick:      5,
            color:      col);
    }

    private static void DrawO(Rectangle r, Color col)
    {
        r = r.Shrink(5);
        DrawRing(
            center:         new((int)(r.X + r.Size.X / 2), (int)(r.Y + r.Size.Y / 2)),
            innerRadius:    r.Size.X / 2 - 5,
            outerRadius:    r.Size.X / 2,
            startAngle:     0,
            endAngle:       360,
            segments:       100,
            color:          col);
    }
    
    public static void Draw3x3(Rectangle r, Color col, float thick)
    {
        var s = r.Size.X;
        var third = s / 3.0f;
        DrawLineEx(
            new(r.X + third, r.Y),
            new(r.X + third, r.Y + s),
            thick,
            col
        );
        DrawLineEx(
            new(r.X + 2 * third, r.Y),
            new(r.X + 2 * third, r.Y + s),
            thick,
            col
        );
        DrawLineEx(
            new(r.X, r.Y + third),
            new(r.X + s, r.Y + third),
            thick,
            col
        );
        DrawLineEx(
            new(r.X, r.Y + 2 * third),
            new(r.X + s, r.Y + 2 * third),
            thick,
            col
        );
    }
}