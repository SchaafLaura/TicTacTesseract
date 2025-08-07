using Raylib_cs;

namespace TicTacTesseract.Scenes;
internal static class SceneManager
{
    private static Scenes currentScene = Scenes.LOADING;
    private static Scene[] scenes;
    static SceneManager()
    {
        scenes = new Scene[5];
        scenes[(int)Scenes.LOADING]     = new LoadingScene();
        scenes[(int)Scenes.MAIN_MENU]   = new MainMenuScene();
        scenes[(int)Scenes.CREATE_GAME] = new CreateGameScene();
        scenes[(int)Scenes.GAME]        = new GameScene();
        scenes[(int)Scenes.CLOSING]     = new ClosingScene();
    }

    public static void ChangeScene(Scenes newScene)
    {
        currentScene = newScene;
    }

    public static void Update()
    {
        scenes[(int)currentScene].Update();
    }

    public static async Task HandleInput()
    {
        await scenes[(int)currentScene].HandleInput();
    }

    public static RenderTexture2D GetTexture() => scenes[(int)currentScene].GetTexture();
}