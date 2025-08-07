namespace TicTacTesseract;

internal static class GameManager
{
    public static Guid GameID;
    public static int Depth;
    public static List<int> Game = [];
    public static HashSet<int> Occupied = [];
    
    public static HashSet<(int, int)> PlayerAMoves = [];
    public static HashSet<(int, int)> PlayerBMoves = [];
    
    public static async Task StartNewGame()
    {
        // starting game
        Console.WriteLine("Starting Game...");
        
        // connecting
        Database.Connect();
        Console.WriteLine("Connected to database");
        
        // API call to get game ID
        await Task.Delay(RNG.rng.Next(200, 1500));
        Console.WriteLine("Got game ID");
        GameID = Guid.NewGuid();
        
        // API call to create game
        await Task.Delay(RNG.rng.Next(200, 1500));
        Console.WriteLine("Game created on server");
        
        // TODO: splitting getting the the game id and creating the game could cause two people creating the same game
        // done?
    }

    public static void ResetFields()
    {
        GameID = Guid.Empty;
        Depth = -1;
        Game = [];
        Occupied = [];

        PlayerAMoves = [];
        PlayerBMoves = [];
    }
}