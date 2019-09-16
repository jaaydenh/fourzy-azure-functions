using System.Collections.Generic;
using Newtonsoft.Json;

public class Game
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string FirstPlayerId { get; set; }
    public string GameStateData { get; set; }
    public string GameStateDataCurrent { get; set; }
    public List<string> PlayerTurnRecord { get; set; }
}