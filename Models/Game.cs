using System.Collections.Generic;
using Newtonsoft.Json;
using FourzyGameModel.Model;

public class Game
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public FourzyGameModel.Model.GameStateData InitialGameStateData { get; set; }
    public FourzyGameModel.Model.GameStateData CurrentGameStateData { get; set; }
    public List<PlayerTurn> PlayerTurnRecord { get; set; }
}