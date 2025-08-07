using Raylib_cs;
using static Raylib_cs.Raylib;
namespace TicTacTesseract.Scenes;
internal class LoadingScene : Scene
{
    public LoadingScene()
    {
        _t = LoadRenderTexture(Settings.WindowWidth, Settings.WindowHeight);
        BeginTextureMode(_t);
        ClearBackground(Color.Pink);
        DrawText("Loading", 400, 450, 50, Color.Black);
        EndTextureMode();
    }

    private int counter = 0;
    public override void Update()
    {
        counter++;
        if (counter != 10) return;
        counter++;
        SceneManager.ChangeScene(Scenes.MAIN_MENU);

    }

    public override Task HandleInput()
    {
        return null;
    }
}