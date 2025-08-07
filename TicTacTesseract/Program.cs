using Raylib_cs;
using TicTacTesseract;
using TicTacTesseract.Scenes;
using static Raylib_cs.Raylib;

InitWindow(Settings.WindowWidth, Settings.WindowHeight, "TicTacTesseract");
SetTargetFPS(60);

var boardInverter = LoadRenderTexture(Settings.WindowWidth, Settings.WindowHeight);
while (!WindowShouldClose() && !Settings.Quit)
{
    /*
    if (Util.Contains3InARow(playerAMoves))
        Console.WriteLine("player A wins");
    if(Util.Contains3InARow(playerBMoves))
        Console.WriteLine("player B wins");
    */

    

    SceneManager.Update();
    SceneManager.HandleInput();
    
    BeginTextureMode(boardInverter);
    DrawTexture(SceneManager.GetTexture().Texture, 0, 0, Color.White);
    EndTextureMode();

    BeginDrawing();
    DrawTexture(boardInverter.Texture, 0, 0, Color.White);
    EndDrawing();
}

Console.WriteLine("byeeeeeeeeeee");
CloseWindow();