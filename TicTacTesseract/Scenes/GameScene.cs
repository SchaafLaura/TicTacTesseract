using Raylib_cs;
using static Raylib_cs.Raylib;

namespace TicTacTesseract.Scenes;

internal class GameScene : Scene
{
    public GameScene()
    {
        _t = LoadRenderTexture(Settings.WindowWidth, Settings.WindowHeight);
        BeginTextureMode(_t);
        ClearBackground(Color.White);
        EndTextureMode();

        GameManager.ResetFields();

        GameManager.Depth = 2;
        for (var i = 0; i < GameManager.Depth + 2; i++)
            GameManager.Game.Add(0);
        GameManager.Occupied.Add(Util.HashMove(GameManager.Game));
        
        BoardDrawer.Setup(
            bounds: new Rectangle(0, 0, Settings.WindowHeight, Settings.WindowHeight), 
            depth:  GameManager.Depth, 
            /*colors: [new Color(80, 70, 70), new Color(117, 117, 127), new Color(192, 182, 182), Color.Beige]);*/
            colors: [Color.Black, Color.Black, Color.Black, Color.Black]);
        BoardDrawer.DrawInitialMove(GameManager.Game);
        
    }
    
    public override void Update()
    {
        BoardDrawer.ClearPreview();
        BoardDrawer.ClearHighlight();
        var nextBoard = GameManager.Game.Take(^(GameManager.Depth + 1)..).ToArray();
        var r = BoardDrawer.GetBoard(nextBoard);
        var mousePos = GetMousePosition();
        BoardDrawer.Highlight(nextBoard);
        
        if (r.Contains(mousePos))
        {
            var mouseBoardIndex = BoardDrawer.PixelPositionToBoardIndex(mousePos);
            if (mouseBoardIndex.Length == GameManager.Depth + 2 && !GameManager.Occupied.Contains(Util.HashMove(mouseBoardIndex)))
            {
                var otherNextBoard = mouseBoardIndex.Skip(1).ToArray();
                BoardDrawer.Highlight(nextBoard, otherNextBoard);
                BoardDrawer.PreviewMove(GameManager.Game, mouseBoardIndex[^1]);

                if (IsMouseButtonPressed(MouseButton.Left))
                {
                    GameManager.Game.Add(mouseBoardIndex[^1]);
                    GameManager.Occupied.Add(Util.HashMove(mouseBoardIndex));

                    if (GameManager.Game.Count % 2 == GameManager.Depth % 2)
                        GameManager.PlayerAMoves.Add(Util.MoveToXY(mouseBoardIndex));
                    else
                        GameManager.PlayerBMoves.Add(Util.MoveToXY(mouseBoardIndex));
                
                    BoardDrawer.DrawNewestMove(GameManager.Game);
                }
            }
        }
        
        BeginTextureMode(_t);
        Util.DrawTextureInverted(BoardDrawer.BoardBackground.Texture, 0, 0);
        Util.DrawTextureInverted(BoardDrawer.PlayerMoves.Texture, 0, 0);
        Util.DrawTextureInverted(BoardDrawer.HighlightOverlay.Texture, 0, 0);
        Util.DrawTextureInverted(BoardDrawer.MovePreview.Texture, 0, 0);
        EndTextureMode();
    }

    public override Task HandleInput()
    {
        return null;
    }
}