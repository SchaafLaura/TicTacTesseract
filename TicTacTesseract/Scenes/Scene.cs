using Raylib_cs;
namespace TicTacTesseract.Scenes;
internal abstract class Scene
{
    protected RenderTexture2D _t;
    public RenderTexture2D GetTexture() => _t;
    public abstract void Update();
    public abstract Task HandleInput();
}