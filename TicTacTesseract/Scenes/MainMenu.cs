using Raylib_cs;
using static Raylib_cs.Raylib;
namespace TicTacTesseract.Scenes;

internal class MainMenuScene : Scene
{
    private List<Button> buttons;
    
    public MainMenuScene()
    {
        buttons = [
            new Button(
                new Rectangle(Settings.WindowWidth / 2.0f - 100, Settings.WindowHeight / 2.0f, 200, 50),
                "Create Game",
                () => { SceneManager.ChangeScene(Scenes.CREATE_GAME); }
            ),
            new Button(
                new Rectangle(Settings.WindowWidth / 2.0f - 100, Settings.WindowHeight / 2.0f + 100, 200, 50),
                "Quit",
                () => { SceneManager.ChangeScene(Scenes.CLOSING); Settings.Quit = true; }
            ),
        ];
        
        _t = LoadRenderTexture(Settings.WindowWidth, Settings.WindowHeight);
        BeginTextureMode(_t);
        ClearBackground(new Color(0.1f, 0.1f, 0.1f));
        EndTextureMode();
    }
    public override void Update()
    { 
        BeginTextureMode(_t);
        
        foreach(var b in buttons)
            b.Display();
        
        EndTextureMode();
    }
    

    public override async Task<Scenes> HandleInput()
    {
        foreach(var b in buttons)
            b.TryClick();
        return Scenes.MAIN_MENU;
    }
}