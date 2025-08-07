using System.Text;
using Newtonsoft.Json;
namespace TicTacTesseract;

internal enum EventID
{
    CREATE_GAME,
    MOVE,
    CLOSE_GAME,
}

internal static class Database
{
    private const string HighscoreGetURL = "https://code-fueled.com/api/tttGET.php";
    private const string InsertScoreURL = "https://code-fueled.com/api/tttINSERT.php";

    private static HttpClient client = new();
    // id, time, event_id, game_id, move_id
    public static async Task Connect()
    {
    /*    var dict = new Dictionary<string, int>
        {
            { "event_id", 99 },
            { "game_id", 123 },
            { "move_id", 3141 },
        };
        var jsonString = JsonConvert.SerializeObject(dict);
        var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        await client.PutAsync(InsertScoreURL, httpContent);

        var response = await client.GetAsync(HighscoreGetURL);
        var responseBody = await response.Content.ReadAsStringAsync();*/
    }
}