using Newtonsoft.Json;

namespace FourzyAzureFunctions
{
    public class CreateGameRequest
    {
        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("opponent")]
        public Player Opponent { get; set; }
    }
}
