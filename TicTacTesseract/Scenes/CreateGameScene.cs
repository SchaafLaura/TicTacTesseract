using Raylib_cs;
using static Raylib_cs.Raylib;

namespace TicTacTesseract.Scenes;
internal class CreateGameScene : Scene
{
    public CreateGameScene()
    {
        _t = LoadRenderTexture(Settings.WindowWidth, Settings.WindowHeight);
        BeginTextureMode(_t);
        ClearBackground(new Color(0.1f, 0.1f, 0.3f));
        EndTextureMode();
    }
    public override void Update()
    {
        
    }

    public override Task HandleInput()
    {
        return null;
    }
}