using Newtonsoft.Json;
using FourzyGameModel.Model;

namespace FourzyAzureFunctions
{
    public class TakeTurnRequest
    {
        [JsonProperty("playerId")]
        public string PlayerId { get; set; }

        [JsonProperty("gameId")]
        public string GameId { get; private set; }
        
        [JsonProperty("turn")]
        public PlayerTurn Turn { get; set; }
    }
}
