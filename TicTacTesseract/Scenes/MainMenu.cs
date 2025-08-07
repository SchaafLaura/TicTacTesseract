using Raylib_cs;
using static Raylib_cs.Raylib;
namespace TicTacTesseract.Scenes;

internal class MainMenuScene : Scene
{
    private readonly Rectangle _btnStartGame;
    private readonly Rectangle _btnQuitGame;
    
    public MainMenuScene()
    {
        _btnStartGame = new Rectangle(Settings.WindowWidth / 2.0f - 100, Settings.WindowHeight / 2.0f, 200, 50);
        _btnQuitGame = new Rectangle(Settings.WindowWidth / 2.0f - 100, Settings.WindowHeight / 2.0f + 100, 200, 50);
        
        _t = LoadRenderTexture(Settings.WindowWidth, Settings.WindowHeight);
        BeginTextureMode(_t);
        ClearBackground(new Color(0.1f, 0.1f, 0.1f));
        EndTextureMode();
    }
    public override void Update()
    { 
        BeginTextureMode(_t);
        
        var col = _btnStartGame.Contains(GetMousePosition()) ? Color.Gray : Color.Black;
        DrawRectangleLinesEx(_btnStartGame, 5, col);
        DrawText("Create", (int)_btnStartGame.Position.X + 20, (int)_btnStartGame.Position.Y + 3, 50, col);
        
        col = _btnQuitGame.Contains(GetMousePosition()) ? Color.Gray : Color.Black;
        DrawRectangleLinesEx(_btnQuitGame, 5, col);
        DrawText("Quit", (int)_btnQuitGame.Position.X + 20, (int)_btnQuitGame.Position.Y + 3, 50, col);
        
        EndTextureMode();
    }
    

    public override async Task<Scenes> HandleInput()
    {
        if (!IsMouseButtonPressed(MouseButton.Left))
            return Scenes.MAIN_MENU;
        if (_btnStartGame.Contains(GetMousePosition()))
        {
            SceneManager.ChangeScene(Scenes.LOADING);
            //await GameManager.StartNewGame();
            //SceneManager.ChangeScene(Scenes.GAME);
            SceneManager.ChangeScene(Scenes.CREATE_GAME);
            return Scenes.GAME;
        }

        if (_btnQuitGame.Contains(GetMousePosition()))
        {
            SceneManager.ChangeScene(Scenes.CLOSING);
            Settings.Quit = true;
            return Scenes.CLOSING;
        }
        return Scenes.MAIN_MENU;
    }
}